using UnityEngine;

public abstract class StunState : State
{
    protected float stunTime;

    public StunState(CharacterScript c) : base(c) { }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //Debug.Log("pain");
        character.Hit = false;

        character.RB2D.linearVelocityX = 0;
        if (character.HitFromLeft)
        {
            character.RB2D.AddForceX(character.AtkTaken.Knockback);
        }
        else
        {
            character.RB2D.AddForceX(-character.AtkTaken.Knockback);
        }
    }

    public override int StateAction()
    {
        if (stateMach.StateTime >= stunTime)
        {
            if (character.Blocking)
            {
                return (int)GeneralStates.BLOCK;
            }
            else
            {
                return character.Direction.y < 0 ? (int)GeneralStates.CROUCH:(int)GeneralStates.IDLE;
            }
        }

        return nextStateId;
    }
}
