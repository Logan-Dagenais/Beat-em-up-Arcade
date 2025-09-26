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
        animName = character.Direction.y < 0 ? "CrouchBlock" : "Block";
        base.StartState(prevState);
    }

    public override int StateAction()
    {
        base.StateAction();

        //  if holding crouch, character is blocking low
        bool blockLow = character.Direction.y < 0;

        if ((blockLow && animName == "Block") ||
            (!blockLow && animName == "CrouchBlock"))
        {
            StartState(prevStateId);
        }

        //  check for if character was blocking properly or if block broken
        if (character.Hit)
        {
            if (character.GuardIntegrity <= 0)
            {
                character.GuardBreak = true;
                return (int)GeneralStates.HITSTUN;
            }

            if (character.Facingleft != character.HitFromLeft || character.AtkTaken.Low != blockLow || character.AtkTaken.Unblockable)
            {
                return (int)GeneralStates.HITSTUN;
            }

            /*
            if (character.AtkTaken.Heavy)
            {
                character.GuardBreak = true;
                return (int)GeneralStates.HITSTUN;
            }
            */

            return (int)GeneralStates.BLOCKSTUN;
        }

        if (!character.Blocking)
        {
            return blockLow ? (int)GeneralStates.CROUCH:(int)GeneralStates.IDLE;
        }

        if (character.Direction.x < 0)
        {
            character.SwitchSpriteDirection(true);
        }
        else if (character.Direction.x > 0)
        {
            character.SwitchSpriteDirection(false);
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.Hit = false;
    }
}
