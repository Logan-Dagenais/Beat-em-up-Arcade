using UnityEngine;

public class PushState : State
{
    public PushState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.PUSH;
        stateMach = c.BehaviorStateMach;
    }

    public override int StateAction()
    {
        return (int)BehaviorStates.DEFENSIVE;
    }

    public override void EndState()
    {
        base.EndState();
        character.AtkHeavy = true;
    }
}
