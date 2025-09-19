using UnityEngine;

abstract public class EnemyScript : CharacterScript
{
    public StateMachine BehaviorStateMach;

    public Transform PlayerTransform;

    //  changes during combat depending on mode
    public float CombatRange;
    
    //  combat attributes for behavior, stays the same
    public float EngagementRange;
    public float AttackRange;
    public bool PlayerToLeft => PlayerTransform.position.x < transform.position.x;
    public bool InCombatRange => transform.position.x <= PlayerTransform.transform.position.x + CombatRange &&
                                 transform.position.x >= PlayerTransform.transform.position.x - CombatRange;

    protected void Awake()
    {
        base.Awake();
        CombatRange = EngagementRange;
    }
}

//  list of behaviors enemies can have
//  basically how they decide what inputs to do
public enum BehaviorStates
{
    //  for when the enemy is withn ideal combat range
    DEFENSIVE,
    OFFENSIVE,

    //  movement for traversing to target range
    CHASE,
    FLEE
}
