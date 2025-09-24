using UnityEngine;

public class MeleeEnemy : EnemyScript
{
    protected void Awake()
    {
        base.Awake();

        WalkBackwards = true;

        PlayerTransform = FindAnyObjectByType<PlayerScript>().transform;

        BehaviorStateMach = gameObject.AddComponent<StateMachine>();

        //  behavior states
        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new DefenseState(this)},

            {(int)BehaviorStates.OFFENSIVE,
            new OffenseState(this)},

            {(int)BehaviorStates.CHASE,
            new ChaseState(this)}
        };
    }

    private void Start()
    {
        SwitchSpriteDirection(PlayerToLeft);
    }
}
