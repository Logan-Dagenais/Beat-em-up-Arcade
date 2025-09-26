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

    //  movement for traversing to target range
    CHASE,
    FLEE
}

abstract public class State
{
    //  should get this from CharacterScript.GeneralStates
    [HideInInspector] public int Id;

    protected bool stateComplete;

    protected bool animPlaying;
    protected float animTiming;
    [SerializeField] protected string animName;

    protected int prevStateId;
    protected int nextStateId;

    protected CharacterScript character;
    protected StateMachine stateMach;

    public State(CharacterScript c)
    {
        character = c;
        stateMach = c.StateMach;

        //  theoretically, all names of animations should match the state scripts
        animName = GetType().Name;
        animName = animName.Substring(0, animName.Length - 5);
    }

    public void SetCharacter(CharacterScript c)
    {
        character = c;
        stateMach = c.StateMach;
    }

    public virtual void StartState(int prevState)
    {
        stateComplete = false;
        prevStateId = prevState;
        nextStateId = Id;
        stateMach.StateTime = 0;
        animPlaying = false;


        character.Anim.Play(animName);
        
    }

    //  this should only be ran in FixedUpdate()
    public virtual int StateAction()
    {
        //  annoying i have to do things this way, for some reason putting
        //  this functionality in start still results in the clip info
        //  being of the previous playing animation
        if (!animPlaying)
        {
            AnimatorClipInfo[] clipInfo = character.Anim.GetCurrentAnimatorClipInfo(0);
            animTiming = clipInfo.Length > 0 ? clipInfo[0].clip.length : .5f;
            animPlaying = true;
        }

        return nextStateId;
    }

    public virtual void EndState()
    {
        stateComplete = true;
    }
}
