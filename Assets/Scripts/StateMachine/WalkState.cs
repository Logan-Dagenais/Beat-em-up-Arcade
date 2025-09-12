using UnityEngine;

public class WalkState : State
{
    public WalkState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.WALK;
    }

    public override int StateAction()
    {
        if (!character.OnGround || character.Direction.y > 0)
        {
            return (int)GeneralStates.AIR;
        }

        if (character.Direction.y < 0)
        {
            return (int)GeneralStates.CROUCH;
        }

        if (character.Direction.x == 0)
        {
            return (int)GeneralStates.IDLE;
        }

        if (character.AtkLight)
        {
            return (int)GeneralStates.ATKLIGHT;
        }

        if (character.AtkHeavy)
        {
            return (int)GeneralStates.ATKHEAVY;
        }

        if (character.Blocking)
        {
            return (int)GeneralStates.BLOCK;
        }

        if (character.Hit)
        {
            return (int)GeneralStates.HITSTUN;
        }

        character.RB2D.linearVelocityX = character.Direction.x * character.WalkSpeed;

        return nextStateId;
    }

}
