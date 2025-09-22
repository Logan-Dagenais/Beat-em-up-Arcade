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

        character.Anim.Play("Idle");

        character.Velocity.y = 0;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (!character.OnGround || character.Direction.y > 0)
        {
            return (int)GeneralStates.AIR;
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
            return (int)GeneralStates.ATKLIGHT;
        }

        if (character.AtkHeavy)
        {
            character.AtkHeavy = false;
            return (int)GeneralStates.ATKHEAVY;
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
