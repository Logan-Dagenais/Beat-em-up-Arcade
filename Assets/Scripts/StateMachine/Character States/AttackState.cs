using NUnit.Framework;
using UnityEngine;

public struct AttackProperties
{
    /*  assuming we are using animator for these
    public int StartFrames;
    public int ActiveFrames;
    public int RecoverFrames;
    */

    //  not sure if we need this but i am preemptively putting this here just in case
    public string AtkAnimName;

    public float Damage;

    // hitstun and blockstun are counted in seconds
    public float Hitstun;
    public float Blockstun;

    //  might make knockback a vector2 instead?
    public float Knockback;
    public bool CanKnockdown;
    public bool Heavy;
    public bool Low;

    public AttackProperties(string atkAnimName, float damage, float hitstun, float blockstun, float knockback, bool knockdown, bool heavy, bool low)
    {
        AtkAnimName = atkAnimName;
        Damage = damage;
        Hitstun = hitstun;
        Blockstun = blockstun;
        Knockback = knockback;
        CanKnockdown = knockdown;
        Heavy = heavy;
        Low = low;


        // AtkAnimName = 
    }
}

public class AttackState : State
{
    private StateMachine attackStates;
    public AttackProperties Properties;

    public AttackState(CharacterScript c, int id, AttackProperties properties) : base(c)
    {
        Id = id;
        Properties = properties;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        /*
        if (Properties.Heavy)
        {
            character.Anim.SetTrigger("AtkHvy");
        }
        else
        {
            character.Anim.SetTrigger("AtkLight");
        }
        */

        character.Anim.Play(Properties.AtkAnimName);

        //tempTime = character.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        //Debug.Log(character.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);

        character.EnemiesHit.Clear();

        //  for testing purposes, we will probably let the animator handle this
        character.Hitboxes.SetActive(true);
    }

    public override int StateAction()
    {
        base.StateAction();

        //  cancels air attack when character touches ground
        //  switches to idle after attack animation ends
        if (((Id == (int)GeneralStates.ATKLIGHTAIR || Id == (int)GeneralStates.ATKHEAVYAIR) &&
            character.OnGround) ||
            (animTiming <= stateMach.StateTime && character.OnGround))
        {
            return (int)GeneralStates.IDLE;
        }

        //  attack interruption
        if (character.Hit)
        {
            character.Hitboxes.SetActive(false);
            return (int)GeneralStates.HITSTUN;
        }

        //  test attack state, remove this later
        if (character.OnGround && !(character.AtkHeavy || character.AtkLight))
        {
           // return (int)GeneralStates.IDLE;
        }

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        //  for testing purposes, we will probably let the animator handle this
        character.Hitboxes.SetActive(false);
    }
}
