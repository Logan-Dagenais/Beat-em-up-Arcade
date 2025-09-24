using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TestDummy : CharacterScript
{
    private void Awake()
    {
        base.Awake();

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
                new AttackProperties(5, .5f, .25f, 100, false, false, false))},

            {(int)GeneralStates.ATKHEAVY,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVY,
                new AttackProperties(10, 1f, .5f, 200, false, true, false))},

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
}
