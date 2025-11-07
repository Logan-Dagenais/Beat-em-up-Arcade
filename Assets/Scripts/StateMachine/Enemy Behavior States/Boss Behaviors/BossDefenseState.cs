using UnityEngine;

public class BossDefenseState : DefenseState
{
    public BossDefenseState(BossScript c) : base(c)
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
            jumpRNG <= ((BossScript)character).JumpChance)
        {
            return (int)BehaviorStates.JUMPSMASH;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
    }
}
