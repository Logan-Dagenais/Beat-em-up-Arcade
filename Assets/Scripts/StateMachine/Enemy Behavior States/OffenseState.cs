using UnityEngine;

public class OffenseState : State
{
    //  for if enemy can not reach the player for too long
    private float defensiveTimer;

    public OffenseState(CharacterScript c) : base(c)
    {
        Id = (int)BehaviorStates.OFFENSIVE;
        stateMach = ((EnemyScript)c).BehaviorStateMach;
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

        tempTimer = 0;
    }

    //  haven't added animations yet, so will need to use this to time attacks
    private float tempTimer;

    public override int StateAction()
    {

        if (((EnemyScript)character).InCombatRange)
        {
            character.AtkLight = true;
        }

        if (character.EnemiesHit.Count > 0)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        //  temporary timer for attacks since haven't added animation timings yet
        /*
        if (character.AtkLight)
        {
            if (tempTimer < .5f)
            {
                tempTimer += Time.deltaTime;
            }
            else
            {
                return (int)BehaviorStates.DEFENSIVE;
            }
        }
        */

        if (defensiveTimer < stateMach.StateTime)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        return nextStateId;
    }

    public override void EndState()
    {

        ((EnemyScript)character).CombatRangeDistance = ((EnemyScript)character).EngagementRange;

        character.AtkLight = false;

        base.EndState();
    }
}
