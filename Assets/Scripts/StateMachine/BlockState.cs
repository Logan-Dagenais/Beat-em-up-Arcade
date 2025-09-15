using UnityEngine;

public class BlockState : State
{

    public BlockState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.BLOCK;
    }

    public override int StateAction()
    {
        //  if holding crouch, character is blocking low
        bool blockLow = character.Direction.y < 0;

        character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, 1f);

        if (!character.Blocking)
        {
            return blockLow ? (int)GeneralStates.CROUCH:(int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.Hit = false;
    }
}
