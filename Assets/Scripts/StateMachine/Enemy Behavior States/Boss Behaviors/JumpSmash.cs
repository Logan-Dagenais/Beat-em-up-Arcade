using UnityEngine;

public class JumpSmashState : State
{
    public JumpSmashState(EnemyScript c) : base(c)
    {
        stateMach = c.BehaviorStateMach;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Direction.y = 1;
    }

    public override int StateAction()
    {


        return nextStateId;
    }

    public override void EndState()
    {
        
        
        base.EndState();
    }
}
