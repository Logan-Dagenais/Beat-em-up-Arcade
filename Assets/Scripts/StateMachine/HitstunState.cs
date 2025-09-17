using UnityEngine;

public class HitstunState : StunState
{
    private bool knockedDown;

    private byte comboCounter;

    public HitstunState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.HITSTUN;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //character.Friction = .25f;

        comboCounter++;

        //  take half damage on guardbreak
        character.Health -= !character.GuardBreak ?
            character.AtkTaken.Damage : character.AtkTaken.Damage / 2;

        //  extended hitstun on guardbreak
        stunTime = !character.GuardBreak ?
            character.AtkTaken.Hitstun : character.AtkTaken.Hitstun * 1.25f;

        //  knocks down when hit with a heavy while still in hitstun
        knockedDown = comboCounter >= 2 && character.AtkTaken.Heavy || character.Health <= 0 ?
            true : character.AtkTaken.CanKnockdown;

        //  knocked back upwards slightly when hit in midair
        if (!character.OnGround)
        {
            //character.Velocity.y = 0;
            //character.RB2D.AddForceY(200);
            character.Velocity.y = 15;

            knockedDown = true;

            //  this is literally the only line of code preventing juggling
            //  i think it would be more fun to have air combos
            //character.Hurtboxes.SetActive(false);
        }

        //  replace this with an animation instead of just rotating it
        character.transform.rotation = character.HitFromLeft ?
            Quaternion.AngleAxis(-45, Vector3.forward) : Quaternion.AngleAxis(45, Vector3.forward);

        character.GuardBreak = false;
    }

    public override int StateAction()
    {
        nextStateId = base.StateAction();

        //  when character is hit out of air, they should be
        //  helpless until they hit the ground and go into knockdown state
        //  might need to add some kind of air tech or a way to regain control
        //  in mid air if we add this
        if (character.OnGround && knockedDown)
        {
            return (int)GeneralStates.KNOCKDOWN;
        }


        //  combo hit logic
        //  resets state when hit again
        if (character.Hit)
        {
            StartState(prevStateId);
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
        //character.Friction = 1;

        knockedDown = false;
        comboCounter = 0;

        //  replace this with animation instead of just rotating it
        character.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        //Debug.Log("back to idle");
    }
}
