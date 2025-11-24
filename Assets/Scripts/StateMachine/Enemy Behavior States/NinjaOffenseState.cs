using UnityEngine;

public class NinjaOffenseState : State
{
    //  for if enemy can not reach the player for too long
    private float defensiveTimer = .8f;

    public NinjaOffenseState(EnemyScript c) : base(c)
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

        character.Direction.y = -1;
        character.Direction.x = ((EnemyScript)character).PlayerToLeft ? -1 : 1;
    }

    public override int StateAction()
    {
        if (defensiveTimer < stateMach.StateTime)
        {
            character.AtkLight = true;
        }

        if (defensiveTimer + .1f < stateMach.StateTime ||
            character.StateMach.CurrentState == (int)GeneralStates.HITSTUN) // interruption
        {
            return (int)BehaviorStates.DEFENSIVE;
        }


        return nextStateId;
    }

    public override void EndState()
    {
        if (character.Direction.y < 0)
        {
            character.Direction.y = 0;
        }

        base.EndState();
    }
}
