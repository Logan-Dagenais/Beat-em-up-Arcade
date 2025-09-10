using UnityEngine;

public class HitstunState : State
{
    //  timer test, replace this with code
    //  that hitstun timer from attack properties later
    private float hitstun = 10;
    public float Hitstun { get { return hitstun; } set { hitstun = 0; } }

    public HitstunState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.HITSTUN;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Hit = false;
    }

    public override int StateAction()
    {
        //  combo hit logic
        //  resets state when hit again
        if (character.Hit)
        {
            StartState(prevStateId);
        }

        if (stateMach.StateTime >= hitstun)
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }
}
