using System;
using UnityEngine;

//  list of states that every character in the game can have
public enum GeneralStates
{
    //  movement states
    IDLE,
    WALK,
    AIR,
    CROUCH,
    JUMPSQUAT,
    LANDING,

    //  combat states (woo hoo violence)

    //  list of every attack every character can have
    //  standing
    ATKLIGHT,
    ATKHEAVY,

    //  crouching
    ATKLIGHTCR,
    ATKHEAVYCR,

    //  aerial
    ATKLIGHTAIR,
    ATKHEAVYAIR,

    //  pain
    HITSTUN,
    KNOCKDOWN,
    BLOCKSTUN,

    //  pain avoidance
    BLOCK,
    DODGE
}


//  list of behaviors enemies can have
//  basically how they decide what inputs to do
public enum BehaviorStates
{
    //  for when the enemy is withn ideal combat range
    DEFENSIVE,
    OFFENSIVE,

    //  When player attempts to walk past a melee enemy
    PUSH,

    //  movement for traversing to target range
    CHASE,

    //  unique boss behaviors
    JUMPSMASH,

}

abstract public class State
{
    //  should get this from CharacterScript.GeneralStates
    [HideInInspector] public int Id;

    protected bool stateComplete;

    protected int prevStateId;
    protected int nextStateId;

    protected CharacterScript character;
    protected StateMachine stateMach;

    public State(CharacterScript c)
    {
        character = c;
        stateMach = c.StateMach;
    }

    public void SetCharacter(CharacterScript c)
    {
        character = c;
        stateMach = c.StateMach;
    }

    //  handles hit reaction and super armor
    protected int HitstunTransition(int nextStateId)
    {
        if (character.Hit)
        {
            if (character.SuperArmor && character.Health > 0)
            {
                character.TakeDamage();

                if (character.HitFromLeft)
                {
                    //character.RB2D.AddForceX(character.AtkTaken.Knockback);
                    character.Velocity.x = character.AtkTaken.Knockback;
                }
                else
                {
                    //character.RB2D.AddForceX(-character.AtkTaken.Knockback);
                    character.Velocity.x = -character.AtkTaken.Knockback;
                }

                if (character.AtkTaken.Heavy)
                {
                    character.PlayHeavySound();
                }
                else
                {
                    character.PlayLightSound();
                }

                character.Hit = false;

                return nextStateId;
            }

            character.Hitboxes.gameObject.SetActive(false);
            return (int)GeneralStates.HITSTUN;
        }

        return nextStateId;
    }

    public virtual void StartState(int prevState)
    {
        stateComplete = false;
        prevStateId = prevState;
        nextStateId = Id;
        stateMach.StateTime = 0;
    }

    //  this should only be ran in FixedUpdate()
    public virtual int StateAction()
    {
        return nextStateId;
    }

    public virtual void EndState()
    {
        stateComplete = true;
    }
}
