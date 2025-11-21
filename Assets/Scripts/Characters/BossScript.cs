using UnityEngine;

public class BossScript : EnemyScript
{
    private void Awake()
    {
        base.Awake();
    }

    protected override void SetBehaviorStateList()
    {
        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new DefenseJumpAltState(this)},

            {(int)BehaviorStates.OFFENSIVE,
            new OffenseState(this)},

            {(int)BehaviorStates.PUSH,
            new PushState(this)},

            {(int)BehaviorStates.CHASE,
            new ChaseState(this)},

            {(int)BehaviorStates.JUMP,
            new JumpOffenseState(this)}
        };
    }
}
