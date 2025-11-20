using UnityEngine;

public class JumpOffenseState : State
{
    public JumpOffenseState(EnemyScript c) : base(c)
    {
        Id = (int)BehaviorStates.JUMP;
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
        if (character.Velocity.y < 0)
        {
            character.AtkLight = true;
        }

        if (character.OnGround && stateMach.StateTime > character.JumpSquatTime + .1f)
        {
            return (int)BehaviorStates.DEFENSIVE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        character.AtkLight = false;
        character.Direction = Vector2.zero;
        
        base.EndState();
    }
}
