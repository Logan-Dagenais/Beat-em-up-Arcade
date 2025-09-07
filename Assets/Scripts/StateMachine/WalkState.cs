using UnityEngine;

public class WalkState : State
{
    public WalkState(int id, CharacterScript c) : base(id, c) {}

    public override int StateAction()
    {
        if (!character.onGround || character.direction.y > 0)
        {
            return (int)CharacterScript.GeneralStates.AIR;
        }

        if (character.direction.y < 0)
        {
            return (int)CharacterScript.GeneralStates.CROUCH;
        }

        if (character.direction.x == 0)
        {
            return (int)CharacterScript.GeneralStates.IDLE;
        }

        character.rb.linearVelocityX = character.direction.x * character.walkSpeed;

        return nextStateId;
    }

}
