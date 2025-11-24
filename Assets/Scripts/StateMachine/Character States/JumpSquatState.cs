using UnityEngine;

public class JumpSquatState : CharacterState
{
    public JumpSquatState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.JUMPSQUAT;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        character.Velocity.y = 0;
    }


    public override int StateAction()
    {
        base.StateAction();

        nextStateId = HitstunTransition(nextStateId);

        if (stateMach.StateTime > character.JumpSquatTime)
        {
            character.Velocity.y = character.JumpForce;
            character.Velocity.x = character.Direction.x * character.WalkSpeed;
            return (int)GeneralStates.AIR;
        }

        return nextStateId;
    }
}
