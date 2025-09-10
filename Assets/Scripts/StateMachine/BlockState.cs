using UnityEngine;

public class BlockState : State
{


    public BlockState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.BLOCK;
    }

    public override int StateAction()
    {

        character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, 1f);

        if (!character.Blocking)
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }
}
