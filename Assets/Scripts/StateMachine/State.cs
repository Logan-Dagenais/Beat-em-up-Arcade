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
