using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterScript : MonoBehaviour
{
    //  character attributes
    [SerializeField] private float MaxHealth;
    public float Health;
    [SerializeField] private float MaxGuardIntegrity;
    public float GuardIntegrity;

    public float JumpForce;
    public float WalkSpeed;

    public bool Facingleft;

    public AttackProperties[] AttackList;

    public float Friction;

    //  keeps track of what enemies have already been hit
    //  so that attacks can only hit once on activation
    public List<CharacterScript> EnemiesHit;

    public Vector2 Velocity;

    //  components
    public Rigidbody2D RB2D;

    public StateMachine StateMach;
    public Animator Anim;

    //  collision detection
    public GameObject Hurtboxes;
    public GameObject Hitboxes;

    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private Rigidbody2D.SlideMovement slideMove;

    //  state machine inputs
    public Vector2 Direction;

    public bool OnGround => RB2D.IsTouching(contactFilter);

    [SerializeField] public bool AtkLight;
    [SerializeField] public bool AtkHeavy;
    [SerializeField] public bool Blocking;

    public bool Hit;

    public AttackProperties AtkTaken;
    public bool HitFromLeft;
    public bool GuardBreak;

    [SerializeField] private float GuardIntCooldown;
    public float GuardIntTimer;

    protected void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
        StateMach = GetComponent<StateMachine>();

        Health = MaxHealth;
        GuardIntegrity = MaxGuardIntegrity;

        //  code for making sure character has a hurtbox and hitbox
        //  while ignoring the order
        //  will have to replace this if we decide characters need more children objects
        GameObject child1 = transform.GetChild(0).gameObject;
        GameObject child2 = transform.GetChild(1).gameObject;

        if (child1.CompareTag("Hurtbox"))
        {
            Hurtboxes = child1;
            Hitboxes = child2;
        }
        else
        {
            Hitboxes = child1;
            Hurtboxes = child2;
        }
    }

    //  this function basically takes the attack state data and transfers it
    //  to the target that was hit so it reacts accordingly
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Hurtbox"))
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

    private void FixedUpdate()
    {
        RB2D.Slide(Velocity, Time.deltaTime, slideMove);

        if (OnGround)
        {
            Velocity.x = Mathf.MoveTowards(Velocity.x, 0, Friction);
        }
        else
        {
            Velocity.y = Mathf.MoveTowards(Velocity.y, -60, 2);
        }

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

}

//  list of states that every character in the game can have
public enum GeneralStates
{
    //  movement states
    IDLE,
    WALK,
    AIR,
    CROUCH,

    //  combat states (woo hoo violence)

    //  list of every attack every character can have
    //  standing
    ATKLIGHT,
    ATKHEAVY,

    //  crouching
    ATKLIGHTCR,
    ATKHEAVYCR,

    //  aerial
    ATKLIGHTAIR,
    ATKHEAVYAIR,

    //  pain
    HITSTUN,
    KNOCKDOWN,
    BLOCKSTUN,

    //  pain avoidance
    BLOCK,
    DODGE
}


