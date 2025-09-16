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
    }

    public override int StateAction()
    {
        if (!character.OnGround || character.Direction.y > 0)
        {
            return (int)GeneralStates.AIR;
        }

        //  custom friction because i didn't want to use unity's built in one
        character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, 1f);

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
            return (int)GeneralStates.ATKLIGHT;
        }

        if (character.AtkHeavy)
        {
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
