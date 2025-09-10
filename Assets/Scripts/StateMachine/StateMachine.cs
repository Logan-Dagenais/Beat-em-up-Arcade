using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.VersionControl.Asset;

public class StateMachine : MonoBehaviour
{
    private int currentStateId;
    public int CurrentState {  get { return currentStateId; } set { currentStateId = value; } }

    private int previousStateId;

    public float StateTime;

    private Dictionary<int, State> StateList; 

    public Dictionary<int, State> States { get {return StateList;} set { StateList = value; } }

    private void TransitionState(int newStateId)
    {
        if (currentStateId == newStateId)
        {
            return;
        }
        
        previousStateId = currentStateId;
        StateList[currentStateId].EndState();
        currentStateId = newStateId;
        StateList[currentStateId].StartState(previousStateId);

        print("switching to " + StateList[currentStateId].ToString());
    }

    private const int MAX_TIME = 99;

    private void FixedUpdate()
    {
        int nextStateId = StateList.ContainsKey(currentStateId) ?
            StateList[currentStateId].StateAction() : previousStateId;

        if (StateList.ContainsKey(currentStateId))
        {
            TransitionState(nextStateId);
        }

        if (StateTime < MAX_TIME)
        {
            StateTime += Time.deltaTime;
            StateTime = Mathf.Clamp(StateTime, 0, MAX_TIME);
        }
    }

}
