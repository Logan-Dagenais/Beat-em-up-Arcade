using UnityEngine;

public class IdleState : State
{
    public IdleState(int id, CharacterScript c) : base(id, c)
    {
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);
        Debug.Log("switching to idle state");
    }

    public override int StateAction()
    {
        if (character.direction.x != 0)
        {
            return (int)CharacterScript.GeneralStates.WALK;
        }

        return nextStateId;
    }


}
