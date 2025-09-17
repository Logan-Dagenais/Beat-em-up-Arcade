using UnityEngine;
using UnityEngine.TextCore.Text;

public class AirState : State
{
    //  i am planning to replace this with however long the jumpsquat animation lasts if we decide to add it
    private float jumpSquatTime = .1f;
    private bool jumped;

    public AirState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.AIR;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //  jumped is false if up direction is held at beginning of state
        //  and character is on ground
        jumped =   character.Direction.y > 0 &&
                   character.OnGround ?
                   false : true;
    }

    private void JumpCheck()
    {
        if (jumped ||
            stateMach.StateTime < jumpSquatTime)
        {
            return;
        }

        jumped = true;
        character.RB2D.AddForce(Vector2.up * character.JumpForce);
        character.RB2D.linearVelocityX = character.Direction.x * character.WalkSpeed;
    }

    public override int StateAction()
    {
        if (character.Hit)
        {
            return (int)GeneralStates.HITSTUN;
        }

        if (character.OnGround)
        {
            if (jumped && stateMach.StateTime > jumpSquatTime + 0.1f)
            {
                return character.Direction.x == 0 ?
                    (int)GeneralStates.IDLE : (int)GeneralStates.WALK;
            }

            JumpCheck();
        }

        if (character.AtkLight)
        {
            return (int)GeneralStates.ATKLIGHTAIR;
        }

        if (character.AtkHeavy)
        {
            return (int)GeneralStates.ATKHEAVYAIR;
        }

        return nextStateId;
    }
}
