using NUnit.Framework;
using System;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public struct AttackProperties
{
    /*  assuming we are using animator for these
    public int StartFrames;
    public int ActiveFrames;
    public int RecoverFrames;
    */

    public float Damage;
    public float GuardDamage;

    // hitstun and blockstun are counted in seconds
    public float Hitstun;
    public float Blockstun;

    //  might make knockback a vector2 instead?
    public float Knockback;
    public bool CanKnockdown;

    public bool Heavy;
    public bool Low;

    public bool Unblockable;

    public float HitboxXOffset;

    public float MovementForce;

    public AttackProperties(float damage,
                            float guardDamage,
                            float hitstun,
                            float blockstun,
                            float knockback,
                            bool knockdown,
                            bool heavy,
                            bool low,
                            bool unblockable,
                            float offset,
                            float movement)
    {
        Damage = damage;
        GuardDamage = guardDamage;
        Hitstun = hitstun;
        Blockstun = blockstun;
        Knockback = knockback;
        CanKnockdown = knockdown;
        Heavy = heavy;
        Low = low;
        Unblockable = unblockable;
        HitboxXOffset = offset;
        MovementForce = movement;
    }
}

[Serializable]
public class AttackState : CharacterState
{
    //  putting this here to make it easier to add attacks in inspector
    public GeneralStates AttackID;

    public AttackProperties Properties;

    public AttackState(CharacterScript c, int id, AttackProperties properties) : base(c)
    {
        Id = id;
        Properties = properties;
    }

    public override void StartState(int prevState)
    {
        base.StartState(prevState);

        if(character.OnGround && character.Direction.x != 0)
        {
            character.Velocity.x = Properties.MovementForce == 0 ?
                                   character.Direction.x * character.WalkSpeed :
                                   character.Direction.x * Properties.MovementForce;
        }

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

        //tempTime = character.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        //Debug.Log(character.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);

        //  for testing purposes, we will probably let the animator handle this
        //character.Hitboxes.SetActive(true);

        //  only changes the x offset depending on what direction character is facing
        character.Hitboxes.offset += character.Facingleft ? Vector2.left * Properties.HitboxXOffset : Vector2.right * Properties.HitboxXOffset;
    }

    public override int StateAction()
    {
        base.StateAction();

        if (character.OnGround)
        {
            //  cancels air attack when character touches ground
            //  switches to idle after attack animation ends
            if (Id == (int)GeneralStates.ATKLIGHTAIR || Id == (int)GeneralStates.ATKHEAVYAIR)
            {
                return (int)GeneralStates.LANDING;
            }

            if (animTiming <= stateMach.StateTime)
            {
                return character.Direction.y < 0 ? (int)GeneralStates.CROUCH : (int)GeneralStates.IDLE;
            }
        }
        else
        {
            if (animTiming <= stateMach.StateTime && Id != (int)GeneralStates.ATKLIGHTAIR)
            {
                return (int)GeneralStates.AIR;
            }
        }

        //  attack interruption
        nextStateId = HitstunTransition(nextStateId);

        //  test attack state, remove this later
        /*
        if (character.OnGround && !(character.AtkHeavy || character.AtkLight))
        {
           // return (int)GeneralStates.IDLE;
        }
        */

        return nextStateId;
    }

    public override void EndState()
    {
        base.EndState();

        character.EnemiesHit.Clear();

        //  reset hitbox position
        character.Hitboxes.offset = Vector2.zero;

        character.Hitboxes.gameObject.SetActive(false);
    }
}
