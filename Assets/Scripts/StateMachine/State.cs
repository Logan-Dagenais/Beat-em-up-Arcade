using System;
using UnityEngine;

abstract public class State
{
    //  should get this from CharacterScript.GeneralStates
    public int Id;

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

    public virtual void StartState(int prevState)
    {
        stateComplete = false;
        prevStateId = prevState;
        nextStateId = Id;
        stateMach.StateTime = 0;
    }

    //  this should only be ran in FixedUpdate()
    public abstract int StateAction();

    public virtual void EndState()
    {
        stateComplete = true;
    }
}
