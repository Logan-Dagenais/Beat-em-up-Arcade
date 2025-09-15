using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterScript : MonoBehaviour
{
    //  character attributes
    public float Health;

    public float JumpForce;
    public float WalkSpeed;

    public bool Facingleft;

    public AttackProperties[] AttackList;

    //  components
    public Rigidbody2D RB2D;

    public StateMachine StateMach;
    public Animator Anim;

    //  collision detection
    public GameObject Hurtboxes;
    public GameObject Hitboxes;

    [SerializeField] private ContactFilter2D contactFilter;

    //  state machine inputs
    public Vector2 Direction;

    public bool OnGround => RB2D.IsTouching(contactFilter);

    [SerializeField] public bool AtkLight;
    [SerializeField] public bool AtkHeavy;
    [SerializeField] public bool Blocking;

    public bool Hit;

    protected void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
        StateMach = GetComponent<StateMachine>();


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

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Hurtbox"))
        {
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

            //  checking what direction the attack was from
            bool hitFromLeft = collisionParent.position.x < transform.position.x ? false : true;

            collisionParent.GetComponent<CharacterScript>().PassToHitState(atkData, hitFromLeft);

        }
    }

    //  hit detection stuff kinda messy right now
    //  not sure if this is how you properly use dependency injection
    //  since every character only has one hitstun state anyways
    public void PassToHitState(AttackProperties atkTaken, bool hitFromLeft)
    {
        ((HitstunState)StateMach.StateList[(int)GeneralStates.HITSTUN]).PassToHitState(atkTaken, hitFromLeft);
        Hit = true;
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

    //  pain avoidance
    BLOCK,
    DODGE
}


