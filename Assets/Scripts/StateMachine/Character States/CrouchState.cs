using UnityEngine;

public class CrouchState : State
{
    public CrouchState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.CROUCH;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (character.Blocking)
        {
            return (int)GeneralStates.BLOCK;
        }

        if (character.Direction.y > -1)
        {
            return (int)GeneralStates.IDLE;
        }

        if (character.AtkLight)
        {
            character.AtkLight = false;
            return (int)GeneralStates.ATKLIGHTCR;
        }

        if (character.AtkHeavy)
        {
            character.AtkHeavy = false;
            return (int)GeneralStates.ATKHEAVYCR;
        }

        return nextStateId;
    }
}
