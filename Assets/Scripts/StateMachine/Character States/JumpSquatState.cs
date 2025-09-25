using UnityEngine;

public class JumpSquatState : State
{
    private float jumpSquatTime = .1f;

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

        if (character.Hit)
        {
            return (int)GeneralStates.HITSTUN;
        }

        if (stateMach.StateTime > jumpSquatTime)
        {
            character.Velocity.y = character.JumpForce;
            character.Velocity.x = character.Direction.x * character.WalkSpeed;
            return (int)GeneralStates.AIR;
        }

        return nextStateId;
    }
}
