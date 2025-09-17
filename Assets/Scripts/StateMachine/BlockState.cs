using UnityEngine;

public class BlockState : State
{

    private AttackProperties atkTaken;
    private bool hitFromLeft;

    public BlockState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.BLOCK;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

    }

    public override int StateAction()
    {
        //  if holding crouch, character is blocking low
        bool blockLow = character.Direction.y < 0;

        if (character.Direction.x < 0)
        {
            character.Facingleft = true;
        }
        else if (character.Direction.x > 0)
        {
            character.Facingleft = false;
        }

        //  check for if character was blocking properly or if block broken
        if (character.Hit)
        {
            if (character.Facingleft != character.HitFromLeft || character.AtkTaken.Low != blockLow)
            {
                return (int)GeneralStates.HITSTUN;
            }

            if (character.AtkTaken.Heavy)
            {
                character.GuardBreak = true;
                return (int)GeneralStates.HITSTUN;
            }

            return (int)GeneralStates.BLOCKSTUN;
        }

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
