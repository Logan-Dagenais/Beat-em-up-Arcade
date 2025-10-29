using UnityEngine;

public class JumpSmashState : State
{
    public JumpSmashState(EnemyScript c) : base(c)
    {
        stateMach = c.BehaviorStateMach;
    }


}
