using UnityEngine;

public class WalkState : State
{
    public WalkState(int id, CharacterScript c) : base(id, c)
    {
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);
        Debug.Log("switching to walk state");
    }

    public override int StateAction()
    {
        if (character.direction.x == 0)
        {
            return (int)CharacterScript.GeneralStates.IDLE;
        }

        return nextStateId;
    }

}
