using UnityEngine;

public class IdleState : State
{
    public IdleState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.IDLE;
    }

    public override void StartState(int prevState)
    {
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

        if (character.Direction.y < 0)
        {
            return (int) GeneralStates.CROUCH;
        }

        if (character.Direction.x != 0)
        {
            return (int)GeneralStates.WALK;
        }

        if (character.AtkLight)
        {
            character.AtkLight = false;
            return character.Direction.y >= 0 ? (int)GeneralStates.ATKLIGHT : (int)GeneralStates.ATKLIGHTCR;
        }

        if (character.AtkHeavy)
        {
            character.AtkHeavy = false;
            return character.Direction.y >= 0 ? (int)GeneralStates.ATKHEAVY : (int)GeneralStates.ATKHEAVYCR;
        }

        if (character.Blocking)
        {
            return (int)GeneralStates.BLOCK;
        }

        if (character.Hit)
        {
            return (int)GeneralStates.HITSTUN;
        }

        return nextStateId;
    }


}
