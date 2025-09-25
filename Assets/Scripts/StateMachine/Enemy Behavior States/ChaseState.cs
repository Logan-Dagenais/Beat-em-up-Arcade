using UnityEngine;

public class ChaseState : State
{
    public ChaseState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.CHASE;
        stateMach = c.BehaviorStateMach;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        if (character.StateMach.CurrentState != (int)GeneralStates.KNOCKDOWN)
        {
            character.SwitchSpriteDirection(((EnemyScript)character).PlayerToLeft);
        }

    }

    public override int StateAction()
    {
        //  there is probably a better way than casting like this, try to fix another time
        character.Direction.x = ((EnemyScript)character).Target < character.transform.position.x ? -1 : 1;

        if (((EnemyScript)character).InCombatRange)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        return nextStateId;
    }
}
