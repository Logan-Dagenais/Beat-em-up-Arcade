using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.VersionControl.Asset;

public class StateMachine : MonoBehaviour
{
    private int currentStateId;
    private int previousStateId;

    private Dictionary<int, State> StateList = new(); 

    public Dictionary<int, State> States { get {return StateList;} }

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void TransitionState(int newStateId)
    {
        if (currentStateId != newStateId)
        {
            previousStateId = currentStateId;
            StateList[currentStateId].EndState();
            currentStateId = newStateId;
            StateList[currentStateId].StartState(previousStateId);
        }
    }

    private void FixedUpdate()
    {
        int nextStateId = StateList[currentStateId].StateAction();

        if (StateList.Count > nextStateId)
        {
            TransitionState(nextStateId);
        }
    }

}
