using UnityEngine;

public class ChaseState : State
{
    public ChaseState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.CHASE;
        stateMach = ((EnemyScript)c).BehaviorStateMach;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //  there is probably a better way than casting like this, try to fix another time
        character.Direction.x = ((EnemyScript)character).PlayerToLeft ? -1 : 1;
    }

    public override int StateAction()
    {
        if (((EnemyScript)character).InCombatRange)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        return nextStateId;
    }
}
