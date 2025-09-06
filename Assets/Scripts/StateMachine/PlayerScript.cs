using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : CharacterScript
{
    private PlayerInput input;
    private StateMachine sm;

    InputAction move;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = input.currentActionMap.FindAction("Move");

        sm = GetComponent<StateMachine>();

        //  unfortunately as of now we will need to manually add every state
        //  with this long line
        //  honestly could not figure out a better way for right now
        sm.States.Add((int)CharacterScript.GeneralStates.IDLE,
            new IdleState((int)CharacterScript.GeneralStates.IDLE, this));

        sm.States.Add((int)CharacterScript.GeneralStates.WALK,
            new WalkState((int)CharacterScript.GeneralStates.WALK, this));
    }

    void OnMove()
    {
        direction = move.ReadValue<Vector2>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
