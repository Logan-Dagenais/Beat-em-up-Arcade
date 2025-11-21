using UnityEngine;

public class DefenseJumpAltState : DefenseState
{
    public DefenseJumpAltState(EnemyScript c) : base(c)
    {
    }

    private float jumpRNG;

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        jumpRNG = Random.Range(0, 10f);
    }

    public override int StateAction()
    {
        nextStateId = base.StateAction();

        if (nextStateId == (int)BehaviorStates.OFFENSIVE &&
            jumpRNG < ((EnemyScript)character).JumpChance)
        {
            return (int)BehaviorStates.JUMP;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
    }
}
