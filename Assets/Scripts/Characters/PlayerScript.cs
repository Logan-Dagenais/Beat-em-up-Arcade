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

        //  unfortunately as of now we will need to manually add every state
        //  with this long line
        //  honestly could not figure out a better way for right now

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

            {(int)GeneralStates.ATKLIGHT,
            new AttackState(this,
                (int)GeneralStates.ATKLIGHT,
                new AttackProperties("LightATK", 5, .5f, .25f, 5, false, false, false))},

            {(int)GeneralStates.ATKHEAVY,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVY,
                new AttackProperties("HeavyATK", 10, 1f, .5f, 10, false, true, false))},

            {(int)GeneralStates.ATKLIGHTCR,
            new AttackState(this,
                (int)GeneralStates.ATKLIGHTCR,
                new AttackProperties("CrouchLight", 5, .5f, .25f, 5, false, false, true))},

            {(int)GeneralStates.ATKHEAVYCR,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVYCR,
                new AttackProperties("CrouchHeavy", 10, 1f, .5f, 10, true, true, true))},

            {(int)GeneralStates.ATKLIGHTAIR,
            new AttackState(this,
                (int)GeneralStates.ATKLIGHTAIR,
                new AttackProperties("LightAerial", 5, .5f, .25f, 5, false, false, false))},

            {(int)GeneralStates.ATKHEAVYAIR,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVYAIR,
                new AttackProperties("HeavyAerial", 10, 1f, .5f, 10, true, true, false))},

            {(int)GeneralStates.HITSTUN,
            new HitstunState(this)},

            {(int)GeneralStates.KNOCKDOWN,
            new KnockdownState(this)},

            {(int)GeneralStates.BLOCKSTUN,
            new BlockstunState(this)},

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

}
