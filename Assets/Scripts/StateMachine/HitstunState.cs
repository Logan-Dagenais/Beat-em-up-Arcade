using UnityEngine;

public class HitstunState : State
{
    //  timer test, replace this with code
    //  that hitstun timer from attack properties later
    private float hitstun = 2;
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

        if (hitFromLeft)
        {
            character.RB2D.AddForce(Vector2.right * atkTaken.Knockback);
        }
        else
        {
            character.RB2D.AddForce(Vector2.left * atkTaken.Knockback);
        }

        //  knocks down when hit with a heavy while still in hitstun
        knockedDown = comboCounter >= 2 && atkTaken.Heavy ?
            true : atkTaken.CanKnockdown;

        //  transition to knockdown state when hit in midair
        if (prevState == (int)GeneralStates.AIR)
        {
            knockedDown = true;
            character.Hurtboxes.SetActive(false);
        }
    }

    public override int StateAction()
    {
        if (character.OnGround)
        {
            //  when character is hit out of air, they should be
            //  helpless until they hit the ground and go into knockdown state
            //  unless we really want to add juggle combos then i can change this
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
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();
        knockedDown = false;
        comboCounter = 0;

        //Debug.Log("back to idle");
    }
}
