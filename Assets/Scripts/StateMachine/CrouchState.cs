using UnityEngine;

public class CrouchState : State
{
    public CrouchState(int id, CharacterScript c) : base(id, c) {}

    public override int StateAction()
    {
        if (character.direction.y > -1)
        {
            return (int)CharacterScript.GeneralStates.IDLE;
        }

        character.rb.linearVelocityX = Mathf.MoveTowards(character.rb.linearVelocityX, 0, 1f);

        return nextStateId;
    }
}
