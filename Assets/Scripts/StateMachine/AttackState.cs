using UnityEngine;

public struct AttackProperties
{
    /*  assuming we are using animator for these
    public int StartFrames;
    public int ActiveFrames;
    public int RecoverFrames;
    */

    public AttackProperties(float hitstun, float blockstun, float knockback, bool knockdown, bool heavy)
    {
        Hitstun = hitstun;
        Blockstun = blockstun;
        Knockback = knockback;
        CanKnockdown = knockdown;
        Heavy = heavy;
    }

    // hitstun and blockstun are counted in seconds
    public float Hitstun;
    public float Blockstun;

    public float Knockback;
    public bool CanKnockdown;
    public bool Heavy;
}

public class AttackState : State
{
    private StateMachine attackStates;
    private AttackProperties properties;

    public AttackState(CharacterScript c, int id, AttackProperties properties) : base(c)
    {
        Id = id;
        this.properties = properties;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

    }

    public override int StateAction()
    {

        character.RB2D.linearVelocityX = Mathf.MoveTowards(character.RB2D.linearVelocityX, 0, 1f);

        if (character.OnGround && !(character.AtkHeavy || character.AtkLight))
        {
            return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }
}
