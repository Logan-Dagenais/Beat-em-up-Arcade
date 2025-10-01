using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

abstract public class CharacterScript : MonoBehaviour
{
    //  character attributes
    [Header("character attributes")]
    [SerializeField] protected float MaxHealth;
    public float Health;
    [SerializeField] protected float MaxGuardIntegrity;
    public float GuardIntegrity;

    public float JumpForce;
    public float WalkSpeed;
    public float AirMobilityAccel;

    public bool Facingleft;

    [SerializeField] protected AttackState[] AttackList;

    public float Friction;

    //  note: gravity and terminal velocity are kinda affected by slideMove.gravity
    //  i don't think by much though
    //  also note that these do not use unity's normal gravity scale, i am pretty sure
    //  the measurements are based on the unity unit though.
    [SerializeField] protected float Gravity;
    [SerializeField] protected float TerminalVelocity;

    public Vector2 Velocity;

    //  keeps track of what enemies have already been hit
    //  so that attacks can only hit once on activation
    public List<CharacterScript> EnemiesHit;

    //  collision detection
    [Header("collision detection")]
    public GameObject Hurtboxes;
    public BoxCollider2D Hitboxes;

    [SerializeField] protected ContactFilter2D contactFilter;
    [SerializeField] protected Rigidbody2D.SlideMovement slideMove;

    //  state machine inputs
    [Header("state machine inputs")]
    public Vector2 Direction;

    public bool OnGround => RB2D.IsTouching(contactFilter);

    [SerializeField] public bool AtkLight;
    [SerializeField] public bool AtkHeavy;
    [SerializeField] public bool Blocking;

    //  mainly for enemies
    public bool WalkBackwards;

    public bool Hit;

    public AttackProperties AtkTaken;
    public bool HitFromLeft;
    public bool GuardBreak;

    [SerializeField] protected float GuardIntCooldown;
    public float GuardIntTimer;

    //  components
    [Header("components (they are assigned on awake)")]
    public Rigidbody2D RB2D;

    public StateMachine StateMach;
    public Animator Anim;
    public SpriteRenderer spriteRender;

    protected void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
        StateMach = GetComponent<StateMachine>();
        Anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();

        Health = MaxHealth;
        GuardIntegrity = MaxGuardIntegrity;

        //  code for making sure character has a hurtbox and hitbox
        //  while ignoring the order
        //  will have to replace this if we decide characters need more children objects
        GameObject child1 = transform.GetChild(0).gameObject;
        GameObject child2 = transform.GetChild(1).gameObject;

        //  checking if child1 is hurtbox layer
        if (child1.gameObject.layer == 6)
        {
            Hurtboxes = child1;
            Hitboxes = child2.GetComponent<BoxCollider2D>();
        }
        else
        {
            Hitboxes = child1.GetComponent<BoxCollider2D>();
            Hurtboxes = child2;
        }


        //  unfortunately as of now we will need to manually add every state
        //  with this long line
        //  honestly could not figure out a better way for right now

        //  basic states every character would probably have
        StateMach.StateList = new()
        {
            {(int)GeneralStates.IDLE,
            new IdleState(this)},

            {(int)GeneralStates.WALK,
            new WalkState(this)},

            {(int)GeneralStates.AIR,
            new AirState(this) },

            {(int)GeneralStates.CROUCH,
            new CrouchState(this) },

            {(int)GeneralStates.JUMPSQUAT,
            new JumpSquatState(this) },

            {(int)GeneralStates.HITSTUN,
            new HitstunState(this)},

            {(int)GeneralStates.KNOCKDOWN,
            new KnockdownState(this)},

            {(int)GeneralStates.BLOCKSTUN,
            new BlockstunState(this)},

            {(int)GeneralStates.BLOCK,
            new BlockState(this)}
        };

        for (int i=0; i<AttackList.Length; i++)
        {
            AttackList[i].SetCharacter(this);
            AttackList[i].Id = (int)AttackList[i].AttackID;

            StateMach.StateList.Add(AttackList[i].Id, AttackList[i]);
        }

    }

    //  this function basically takes the attack state data and transfers it
    //  to the target that was hit so it reacts accordingly
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //  ignore method if not hurtbox layer or if tag matches self (so enemies can't hit each other)
        if (collision.gameObject.layer != 6 ||
            collision.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }

        AttackProperties atkData;

        //  checking if current state is an attack
        //  skips method if not
        switch (StateMach.CurrentState)
        {
            case (int)GeneralStates.ATKLIGHT:
            case (int)GeneralStates.ATKHEAVY:

            case (int)GeneralStates.ATKLIGHTCR:
            case (int)GeneralStates.ATKHEAVYCR:

            case (int)GeneralStates.ATKLIGHTAIR:
            case (int)GeneralStates.ATKHEAVYAIR:
                atkData = ((AttackState)StateMach.StateList[StateMach.CurrentState]).Properties;
                break;

            default:
                return;
        }

        Transform collisionParent = collision.transform.parent;
        CharacterScript collisionCharacter = collisionParent.GetComponent<CharacterScript>();

        if (!EnemiesHit.Contains(collisionCharacter))
        {
            EnemiesHit.Add(collisionCharacter);

            //  checking what direction the attack was from
            bool hitFromLeft = collisionParent.position.x < transform.position.x ? false : true;

            collisionCharacter.HitReaction(atkData, hitFromLeft);
        }
    }

    public void HitReaction(AttackProperties atkTaken, bool hitFromLeft)
    {
        AtkTaken = atkTaken;
        HitFromLeft = hitFromLeft;

        Hit = true;
    }

    public virtual void TakeDamage()
    {
        //  take half damage on guardbreak
        Health -= !GuardBreak ?
            AtkTaken.Damage : AtkTaken.Damage / 2;
    }

    public virtual void DeadState()
    {
        Destroy(gameObject, 1.6f);
    }

    private void FixedUpdate()
    {
        if (OnGround)
        {
            Velocity.x = Mathf.MoveTowards(Velocity.x, 0, Friction);

            if (StateMach.CurrentState == (int)GeneralStates.AIR)
            {
                Velocity.y = Mathf.MoveTowards(Velocity.y, -TerminalVelocity, Gravity);
            }
        }
        else
        {
            Velocity.y = Mathf.MoveTowards(Velocity.y, -TerminalVelocity, Gravity);

            switch (StateMach.CurrentState)
            {
                case (int)GeneralStates.HITSTUN:
                case (int)GeneralStates.KNOCKDOWN:
                    Velocity.x = Mathf.MoveTowards(Velocity.x, 0, Friction * .2f);
                    break;
            }
        }

        RB2D.Slide(Velocity, Time.deltaTime, slideMove);

        RecoverGuard();
    }

    public virtual void RecoverGuard()
    {
        if (GuardIntTimer >= GuardIntCooldown)
        {
            GuardIntTimer = 0;
            GuardIntegrity = MaxGuardIntegrity;
        }


        if (GuardIntegrity != MaxGuardIntegrity)
        {
            switch (StateMach.CurrentState)
            {
                case (int)GeneralStates.BLOCKSTUN:
                case (int)GeneralStates.BLOCK:
                    break;

                default:
                    GuardIntTimer += Time.deltaTime;
                    break;
            }

        }
    }

    //  passing in true will face left
    public void SwitchSpriteDirection(bool left)
    {
        Facingleft = left;
        spriteRender.flipX = left;
    }

}