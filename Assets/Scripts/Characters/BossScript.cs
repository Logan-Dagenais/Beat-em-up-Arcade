using UnityEngine;

public class BossScript : EnemyScript
{
    [Range(0, 10)]
    public float JumpChance;

    private void Awake()
    {
        base.Awake();
    }

    protected override void SetBehaviorStateList()
    {
        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new BossDefenseState(this)},

            {(int)BehaviorStates.OFFENSIVE,
            new OffenseState(this)},

            {(int)BehaviorStates.PUSH,
            new PushState(this)},

            {(int)BehaviorStates.CHASE,
            new ChaseState(this)},

            {(int)BehaviorStates.JUMPSMASH,
            new JumpSmashState(this)}
        };
    }
}
