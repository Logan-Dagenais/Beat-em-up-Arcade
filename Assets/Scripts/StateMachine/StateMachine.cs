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

    public Dictionary<int, State> StateList; 

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

        print(gameObject.name+": switching to " + StateList[currentStateId].ToString());
    }

    private const int MAX_TIME = 99;

    private void FixedUpdate()
    {
        int nextStateId = StateList[currentStateId].StateAction();

        if (!StateList.ContainsKey(nextStateId))
        {
            nextStateId = currentStateId;
        }

        TransitionState(nextStateId);

        if (StateTime < MAX_TIME)
        {
            StateTime += Time.deltaTime;
            StateTime = Mathf.Clamp(StateTime, 0, MAX_TIME);
        }
    }

}
