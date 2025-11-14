using UnityEngine;

public class LandingState : CharacterState
{
    public LandingState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.LANDING;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Velocity.y = 0;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (character.Hit)
        {
            return (int)GeneralStates.HITSTUN;
        }

        if (stateMach.StateTime > character.LandingLagTime)
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
    }
}
