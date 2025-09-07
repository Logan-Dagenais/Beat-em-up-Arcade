using UnityEngine;
using UnityEngine.TextCore.Text;

public class AirState : State
{
    public AirState(int id, CharacterScript c) : base(id, c) {}

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        if ((prevState == (int)CharacterScript.GeneralStates.IDLE ||
            prevState == (int)CharacterScript.GeneralStates.WALK) &&
            character.onGround)
        {
            character.rb.AddForce(Vector2.up * character.jumpForce);
            character.rb.linearVelocityX = character.direction.x * character.walkSpeed;
        }
    }

    public override int StateAction()
    {
        if (character.onGround)
        {
            if (character.direction.x == 0)
            {
                return (int)CharacterScript.GeneralStates.IDLE;
            }
            else
            {
                return (int)CharacterScript.GeneralStates.WALK;
            }
        }

        return nextStateId;
    }
}
