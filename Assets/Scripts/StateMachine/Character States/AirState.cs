using UnityEngine;
using UnityEngine.TextCore.Text;

public class AirState : CharacterState
{
    public AirState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.AIR;
    }

    public override int StateAction()
    {
        base.StateAction();

        nextStateId = HitstunTransition(nextStateId);

        if (character.OnGround && character.Velocity.y <= 0)
        {
            return (int)GeneralStates.LANDING;
        }
        else
        {
            character.Velocity.x += character.Direction.x * character.AirMobilityAccel;
            character.Velocity.x = Mathf.Clamp(character.Velocity.x, -character.WalkSpeed, character.WalkSpeed);

            if (character.AtkLight)
            {
                character.AtkLight = false;
                return (int)GeneralStates.ATKLIGHTAIR;
            }

            if (character.AtkHeavy)
            {
                character.AtkHeavy = false;
                return (int)GeneralStates.ATKHEAVYAIR;
            }
        }

        return nextStateId;
    }
}
