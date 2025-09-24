using UnityEngine;

public class EnemyScript : CharacterScript
{
    public StateMachine BehaviorStateMach;

    public Transform PlayerTransform;

    //  changes during combat depending on mode
    public float CombatRangeDistance;

    //  combat attributes for behavior, stays the same
    public float EngagementRange;
    public float AttackRange;
    //  for if the target is not directly the player (mostly for ranged enemies)
    public float TargetOffset;

    public float Target => PlayerToLeft ? PlayerTransform.position.x + TargetOffset : PlayerTransform.position.x - TargetOffset;

    public float CombatRangeMax => Target + CombatRangeDistance;
    public float CombatRangeMin => Target - CombatRangeDistance;

    public bool PlayerToLeft => PlayerTransform.position.x < transform.position.x;
    public bool InCombatRange => transform.position.x <= CombatRangeMax &&
                                 transform.position.x >= CombatRangeMin;

    protected void Awake()
    {
        base.Awake();
        CombatRangeDistance = EngagementRange;

        PlayerTransform = FindAnyObjectByType<PlayerScript>().transform;

        BehaviorStateMach = gameObject.AddComponent<StateMachine>();

        WalkBackwards = true;

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
