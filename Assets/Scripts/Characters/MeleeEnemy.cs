using UnityEngine;

public class MeleeEnemy : EnemyScript
{
    protected void Awake()
    {
        base.Awake();

        PlayerTransform = FindAnyObjectByType<PlayerScript>().transform;

        BehaviorStateMach = gameObject.AddComponent<StateMachine>();

        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new DefenseState(this)},

            {(int)BehaviorStates.OFFENSIVE,
            new OffenseState(this)},

            {(int)BehaviorStates.CHASE,
            new ChaseState(this)}
        };

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
                new AttackProperties(5, .5f, .25f, 5, false, false, false))},

            {(int)GeneralStates.ATKHEAVY,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVY,
                new AttackProperties(10, 1f, .5f, 10, false, true, false))},

            {(int)GeneralStates.ATKLIGHTCR,
            new AttackState(this,
                (int)GeneralStates.ATKLIGHTCR,
                new AttackProperties(5, .5f, .25f, 5, false, false, true))},

            {(int)GeneralStates.ATKHEAVYCR,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVYCR,
                new AttackProperties(10, 1f, .5f, 10, true, true, true))},

            {(int)GeneralStates.ATKLIGHTAIR,
            new AttackState(this,
                (int)GeneralStates.ATKLIGHTAIR,
                new AttackProperties(5, .5f, .25f, 5, false, false, false))},

            {(int)GeneralStates.ATKHEAVYAIR,
            new AttackState(this,
                (int)GeneralStates.ATKHEAVYAIR,
                new AttackProperties(10, 1f, .5f, 10, true, true, false))},

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
