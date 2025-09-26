using UnityEngine;

public class WalkState : State
{
    public WalkState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.WALK;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Velocity.y = 0;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (!character.WalkBackwards)
        {
            if (character.Direction.x < 0)
            {
                character.SwitchSpriteDirection(true);
            }
            else if (character.Direction.x > 0)
            {
                character.SwitchSpriteDirection(false);
            }
        }

        if (!character.OnGround)
        {
            return (int)GeneralStates.AIR;
        }

        if (character.Direction.y > 0)
        {
            return (int)GeneralStates.JUMPSQUAT;
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
            character.AtkLight = false;
            return (int)GeneralStates.ATKLIGHT;
        }

        if (character.AtkHeavy)
        {
            character.AtkHeavy = false;
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

        character.Velocity.x = character.Direction.x * character.WalkSpeed;

        return nextStateId;
    }

}
