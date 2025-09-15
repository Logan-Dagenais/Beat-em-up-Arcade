using UnityEngine;

public class HitstunState : State
{
    //  timer test, replace this with code
    //  that hitstun timer from attack properties later
    private float hitstun;
    public float Hitstun { get { return hitstun; } set { hitstun = value; } }

    private bool knockedDown;

    private byte comboCounter;

    public HitstunState(CharacterScript c) : base(c)
    {
        Id = (int)GeneralStates.HITSTUN;
    }

    /*
     *  this is a little weird and funky but it works
     */
    public void PassToHitState(AttackProperties atkTaken, bool hitFromLeft)
    {
        this.atkTaken = atkTaken;
        this.hitFromLeft = hitFromLeft;
    }

    private AttackProperties atkTaken;
    private bool hitFromLeft;

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        //Debug.Log("pain");
        character.Hit = false;

        comboCounter++;

        character.Health -= atkTaken.Damage;

        hitstun = atkTaken.Hitstun;

        //  knocked back upwards slightly when hit in midair
        if (!character.OnGround)
        {
            character.RB2D.linearVelocityY = 0;
            character.RB2D.AddForceY(200);
        }

        character.RB2D.linearVelocityX = 0;
        if (hitFromLeft)
        {
            character.RB2D.AddForceX(atkTaken.Knockback);
        }
        else
        {
            character.RB2D.AddForceX(-atkTaken.Knockback);
        }

        //  replace this with an animation instead of just rotating it
        character.transform.rotation = hitFromLeft ?
            Quaternion.AngleAxis(-45, Vector3.forward) : Quaternion.AngleAxis(45, Vector3.forward);

        //  knocks down when hit with a heavy while still in hitstun
        knockedDown = comboCounter >= 2 && atkTaken.Heavy ?
            true : atkTaken.CanKnockdown;

        //  transition to knockdown state when hit in midair
        if (prevState == (int)GeneralStates.AIR)
        {
            knockedDown = true;

            //  this is literally the only line of code preventing juggling
            //  i think it would be more fun to have air combos
            //character.Hurtboxes.SetActive(false);
        }
    }

    public override int StateAction()
    {
        if (character.OnGround)
        {
            //  when character is hit out of air, they should be
            //  helpless until they hit the ground and go into knockdown state
            //  might need to add some kind of air tech or a way to regain control
            //  in mid air if we add this
            if (knockedDown)
            {
                return (int)GeneralStates.KNOCKDOWN;
            }

            character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, .25f);
        }


        //  combo hit logic
        //  resets state when hit again
        if (character.Hit)
        {
            StartState(prevStateId);
        }

        if (stateMach.StateTime >= hitstun && !knockedDown)
        {
            if (character.Blocking)
            {
                return (int)GeneralStates.BLOCK;
            }
            else
            {
                return (int)GeneralStates.IDLE;
            }
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
        knockedDown = false;
        comboCounter = 0;

        //  replace this with animation instead of just rotating it
        character.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        //Debug.Log("back to idle");
    }
}
