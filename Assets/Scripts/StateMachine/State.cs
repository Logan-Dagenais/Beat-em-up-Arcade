using System;
using UnityEngine;

abstract public class State
{
    //  should get this from CharacterScript.GeneralStates
    public int Id;
    //  measured in frames, assuming 60 fps cap
    protected int time;
    protected bool stateComplete;

    protected int prevStateId;
    protected int nextStateId;

    protected CharacterScript character;

    public State(int id, CharacterScript c)
    {
        Id = id;
        character = c;
    }

    public virtual void StartState(int prevState)
    {
        stateComplete = false;
        prevStateId = prevState;
        nextStateId = Id;
    }

    //  this should only be ran in FixedUpdate()
    public abstract int StateAction();

    public virtual void EndState()
    {
        stateComplete = true;
        time = 0;
    }
}
