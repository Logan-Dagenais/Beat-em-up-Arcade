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
        if (character.Direction.y < 0)
        {
            animName = "CrouchBlock";
        }
        else
        {
            animName = character.Direction.x == 0 ? "Block" : "BlockWalk";
        }

        base.StartState(prevState);

        character.Velocity.y = 0;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (!character.OnGround && stateMach.StateTime > 0.02f)
        {
            return (int)GeneralStates.AIR;
        }

        if (character.Direction.y > 0)
        {
            return (int)GeneralStates.JUMPSQUAT;
        }

        //  if holding crouch, character is blocking low
        bool blockLow = character.Direction.y < 0;

        if (!blockLow)
        {
            character.Velocity.x = character.Direction.x * character.WalkSpeed / 2;

            if ((character.Direction.x == 0 && animName != "Block") ||
                (character.Direction.x != 0 && animName != "BlockWalk"))
            {
                StartState(prevStateId);
            }
        }
        else if (blockLow)
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
