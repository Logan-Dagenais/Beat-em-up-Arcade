using System;
using UnityEngine;

//  i don't know if this needs to be its own class, might combine it with hitstun state
public class KnockdownState : State
{
    //  adjust later
    private float downTime = 1;
    private int bounce;

    public KnockdownState(CharacterScript c) : base(c) {
        Id = (int)GeneralStates.KNOCKDOWN;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //character.Friction = .25f;
        bounce = 2;

        character.Hurtboxes.SetActive(false);

        //  replace this with an animation
        //character.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (bounce > 0 && character.OnGround)
        {
            character.Velocity.y = 12;
            bounce--;
        }
        else if (character.OnGround)
        {
            character.Velocity.y = 0;
        }

        if (character.Health <= 0)
        {
            character.DeadState();
        }

        if (stateMach.StateTime >= downTime && character.Health > 0)
        {
            return (int)GeneralStates.IDLE;
        }
        else if(stateMach.StateTime >= downTime)
        {
            int tempTime = (int)(stateMach.StateTime * 10);
 
            character.spriteRender.enabled = tempTime % 2 == 0;

        }

            return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        //character.Friction = 1;

        // Debug.Log("back to idle");

        //  replace this with animation
        //character.GetComponent<SpriteRenderer>().color = Color.white;

        character.Hit = false;
        character.Hurtboxes.SetActive(true);
    }

}
