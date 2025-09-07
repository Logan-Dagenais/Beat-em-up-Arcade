using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    protected StateMachine sm;

    public Vector2 direction;
    public bool attacking;
    public bool blocking;

    public float jumpForce;
    public float walkSpeed;

    public Rigidbody2D rb;

    [SerializeField] private ContactFilter2D contactFilter;

    public bool onGround => rb.IsTouching(contactFilter);

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //  states that both player and enemies should have
    public enum GeneralStates
    {
        IDLE,
        WALK,
        AIR,
        CROUCH,
        ATTACK,
        HITSTUN,
        KNOCKDOWN,
        BLOCK,
        DODGE
    }

}
