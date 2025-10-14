using UnityEngine;
using UnityEngine.TextCore.Text;

public class AirState : State
{
    public AirState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.AIR;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (character.Hit)
        {
            return (int)GeneralStates.HITSTUN;
        }

        if (character.OnGround && character.Velocity.y <= 0)
        {
            return character.Direction.x == 0 ?
                (int)GeneralStates.IDLE : (int)GeneralStates.WALK;
        }
        else
        {
            character.Velocity.x += character.Direction.x * character.AirMobilityAccel;
            character.Velocity.x = Mathf.Clamp(character.Velocity.x, -character.WalkSpeed, character.WalkSpeed);

            if (character.AtkLight)
            {
                return (int)GeneralStates.ATKLIGHTAIR;
            }

            if (character.AtkHeavy)
            {
                return (int)GeneralStates.ATKHEAVYAIR;
            }
        }

        return nextStateId;
    }
}
