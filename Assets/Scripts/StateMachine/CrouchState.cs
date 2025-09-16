using UnityEngine;

public class CrouchState : State
{
    public CrouchState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.CROUCH;
    }

    public override int StateAction()
    {
        if (character.Blocking)
        {
            return (int)GeneralStates.BLOCK;
        }

        if (character.Direction.y > -1)
        {
            return (int)GeneralStates.IDLE;
        }

        character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, 1f);

        return nextStateId;
    }
}
