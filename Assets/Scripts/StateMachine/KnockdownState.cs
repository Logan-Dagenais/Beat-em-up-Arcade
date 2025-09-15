using UnityEngine;

//  i don't know if this needs to be its own class, might combine it with hitstun state
public class KnockdownState : State
{
    //  adjust later
    private float downTime = 2;

    public KnockdownState(CharacterScript c) : base(c) {
        Id = (int)GeneralStates.KNOCKDOWN;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Hurtboxes.SetActive(false);

        /*  
         *  possible bug: hit variable might be true after this state ends
         *  but theoretically nothing should be activating it because the
         *  hurtbox should be disabled during this state
         */
        character.Hit = false;

        //  replace this with an animation instead of just rotating it
        character.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
    }

    public override int StateAction()
    {

        character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, .25f);

        if (stateMach.StateTime >= downTime)
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        // Debug.Log("back to idle");

        //  replace this with animation instead of just rotating it
        character.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        character.Hit = false;
        character.Hurtboxes.SetActive(true);
    }

}
