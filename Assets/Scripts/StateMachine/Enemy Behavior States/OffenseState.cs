using UnityEngine;

public class OffenseState : State
{
    //  for if enemy can not reach the player for too long
    private float defensiveTimer;

    public OffenseState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.OFFENSIVE;
        stateMach = c.BehaviorStateMach;
    }


    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        if (character.StateMach.CurrentState != (int)GeneralStates.KNOCKDOWN)
        {
            character.SwitchSpriteDirection(((EnemyScript)character).PlayerToLeft);
        }

        defensiveTimer = Random.Range(((EnemyScript)character).DefenseTimerMin, ((EnemyScript)character).DefenseTimerMax);

        ((EnemyScript)character).CombatRangeDistance = ((EnemyScript)character).AttackRange;

        character.Direction.x = ((EnemyScript)character).PlayerToLeft ? -1 : 1;
    }

    public override int StateAction()
    {

        if (((EnemyScript)character).InCombatRange ||
            defensiveTimer < stateMach.StateTime)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        return nextStateId;
    }

    public override void EndState()
    {

        ((EnemyScript)character).CombatRangeDistance = ((EnemyScript)character).EngagementRange;

        character.AtkLight = true;

        base.EndState();
    }
}
