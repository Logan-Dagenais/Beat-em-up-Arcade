using UnityEngine;

//  i don't know if this needs to be its own class, might combine it with hitstun state
public class KnockdownState : State
{
    //  adjust later
    private float downTime = 1;

    public KnockdownState(CharacterScript c) : base(c) {
        Id = (int)GeneralStates.KNOCKDOWN;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //character.Friction = .25f;

        character.Hurtboxes.SetActive(false);

        /*  
         *  possible bug: hit variable might be true after this state ends
         *  but theoretically nothing should be activating it because the
         *  hurtbox should be disabled during this state
         */
        character.Hit = false;

        //  replace this with an animation
        //character.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (stateMach.StateTime >= downTime && character.Health > 0)
        {
            return (int)GeneralStates.IDLE;
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
