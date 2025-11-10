using UnityEngine;

public class JumpSmashState : State
{
    public JumpSmashState(BossScript c) : base(c)
    {
        Id = (int)BehaviorStates.JUMPSMASH;
        stateMach = c.BehaviorStateMach;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Direction.x = ((EnemyScript)character).PlayerToLeft ? -1 : 1;

        //  jump input
        character.Direction.y = 1;
    }

    public override int StateAction()
    {
        if (character.OnGround && stateMach.StateTime > character.JumpSquatTime + .1f)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        character.Direction = Vector2.zero;
        
        base.EndState();
    }
}
