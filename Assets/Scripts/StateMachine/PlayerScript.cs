using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : CharacterScript
{
    private PlayerInput input;

    private InputAction move;
    private InputAction atkL;
    private InputAction atkH;
    private InputAction block;

    protected void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInput>();
        move = input.currentActionMap.FindAction("Move");
        atkL = input.currentActionMap.FindAction("Light Attack");
        atkH = input.currentActionMap.FindAction("Heavy Attack");
        block = input.currentActionMap.FindAction("Block");

        StateMach = GetComponent<StateMachine>();

        //  unfortunately as of now we will need to manually add every state
        //  with this long line
        //  honestly could not figure out a better way for right now

        StateMach.States = new()
        {
            {(int)GeneralStates.IDLE, 
            new IdleState(this)},

            {(int)GeneralStates.WALK,
            new WalkState(this)},

            {(int)GeneralStates.AIR,
            new AirState(this) },

            {(int)GeneralStates.CROUCH,
            new CrouchState(this) },

            {(int)GeneralStates.ATKLIGHT,
            new AttackState(this, (int)GeneralStates.ATKLIGHT, new AttackProperties(.5f, .25f, 100, false, false))},

            {(int)GeneralStates.ATKHEAVY,
            new AttackState(this, (int)GeneralStates.ATKHEAVY, new AttackProperties(1f, .5f, 200, false, true))},

            {(int)GeneralStates.BLOCK,
            new BlockState(this)}
        };

    }

    void OnMove()
    {
        Direction = move.ReadValue<Vector2>();
    }

    void OnLightAttack()
    {
        AtkLight = atkL.IsPressed();
    }

    void OnHeavyAttack()
    {
        AtkHeavy = atkH.IsPressed();
    }

    void OnBlock()
    {
        Blocking = block.IsPressed();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
