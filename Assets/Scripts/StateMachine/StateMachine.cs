using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.VersionControl.Asset;

public class StateMachine : MonoBehaviour
{
    private int currentStateId;
    private int previousStateId;

    //  measured in frames, assuming 60 fps cap
    protected int time;

    private Dictionary<int, State> StateList = new(); 

    public Dictionary<int, State> States { get {return StateList;} }

    private void TransitionState(int newStateId)
    {
        if (currentStateId != newStateId)
        {
            time = 0;
            previousStateId = currentStateId;
            StateList[currentStateId].EndState();
            currentStateId = newStateId;
            StateList[currentStateId].StartState(previousStateId);

            print("switching to " + StateList[currentStateId].ToString());
        }
    }

    private void FixedUpdate()
    {
        int nextStateId = StateList.ContainsKey(currentStateId) ?
            StateList[currentStateId].StateAction() : previousStateId;

        if (StateList.Count > nextStateId)
        {
            TransitionState(nextStateId);
        }

        // limit for timer
        if (time < 63)
        {
            time++;
        }
    }

}
