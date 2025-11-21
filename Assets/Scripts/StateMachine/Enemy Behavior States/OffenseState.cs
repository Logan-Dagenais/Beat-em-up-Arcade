using UnityEngine;

public class OffenseState : State
{
    //  for if enemy can not reach the player for too long
    private float defensiveTimer;
    private float crouchChanceRng;

    public OffenseState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.OFFENSIVE;
        stateMach = c.BehaviorStateMach;
    }


    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        crouchChanceRng = Random.Range(0f, 10f);

        if (character.StateMach.CurrentState != (int)GeneralStates.KNOCKDOWN)
        {
            character.SwitchSpriteDirection(((EnemyScript)character).PlayerToLeft);
        }

        defensiveTimer = Random.Range(((EnemyScript)character).DefenseTimerMin, ((EnemyScript)character).DefenseTimerMax);

        ((EnemyScript)character).CombatRangeDistance = ((EnemyScript)character).AttackRange;

        character.Direction.x = ((EnemyScript)character).PlayerToLeft ? -1 : 1;

        character.Direction.y = crouchChanceRng <= ((EnemyScript)character).CrouchChance ? -1 : 0;
        //  right now enemy ends offense state crouching. not sure how to reset direction variable
        //  without negating the crouch attack. for some reason behavior states executes faster
        //  than action states or something
        character.AtkLight = character.Direction.y < 0;
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

        character.AtkLight = !(crouchChanceRng <= ((EnemyScript)character).CrouchChance);

        if (character.Direction.y < 0)
        {
            character.Direction.y = 0;
        }

        base.EndState();
    }
}
