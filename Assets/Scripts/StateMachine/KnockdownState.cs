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

        character.Friction = .25f;

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
        if (stateMach.StateTime >= downTime && character.Health > 0)
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.Friction = 1;

        // Debug.Log("back to idle");

        //  replace this with animation instead of just rotating it
        character.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        character.Hit = false;
        character.Hurtboxes.SetActive(true);
    }

}
