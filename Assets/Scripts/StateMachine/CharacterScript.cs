using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterScript : MonoBehaviour
{
    public StateMachine StateMach;
    public Animator Anim;

    public bool Facingleft;

    public Vector2 Direction;

    [SerializeField] public bool AtkLight;
    [SerializeField] public bool AtkHeavy;

    [SerializeField] public bool Blocking;
    public bool Hit;

    public float JumpForce;
    public float WalkSpeed;
    
    public GameObject Hurtboxes;
    public GameObject Hitboxes;

    public Rigidbody2D RB2D;

    [SerializeField] private ContactFilter2D contactFilter;

    public AttackProperties[] AttackList;

    public bool OnGround => RB2D.IsTouching(contactFilter);

    protected void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();

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
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Hit = true;
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

    //  pain avoidance
    BLOCK,
    DODGE
}


