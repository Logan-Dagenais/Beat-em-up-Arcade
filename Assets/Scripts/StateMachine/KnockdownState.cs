using UnityEngine;

//  i don't know if this needs to be its own class, might combine it with hitstun state
public class KnockdownState : State
{
    //  adjust later
    private float downTime = 1;

    public KnockdownState(CharacterScript c) : base(c) { }

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
    }

    public override int StateAction()
    {

        if (stateMach.StateTime >= downTime)
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.Hurtboxes.SetActive(true);
    }

}
