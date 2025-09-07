using UnityEngine;

public class IdleState : State
{
    public IdleState(int id, CharacterScript c) : base(id, c) {}

    public override void StartState(int prevState)
    {
        base.StartState(prevState);
    }

    public override int StateAction()
    {
        if (!character.onGround || character.direction.y > 0)
        {
            return (int)CharacterScript.GeneralStates.AIR;
        }

        character.rb.linearVelocityX = Mathf.MoveTowards(character.rb.linearVelocityX, 0, 1f);

        if (character.direction.y < 0)
        {
            return (int) CharacterScript.GeneralStates.CROUCH;
        }

        if (character.direction.x != 0)
        {
            return (int)CharacterScript.GeneralStates.WALK;
        }

        return nextStateId;
    }


}
