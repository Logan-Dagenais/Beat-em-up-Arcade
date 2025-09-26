using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : CharacterScript
{
    private PlayerInput input;

    private InputAction move;
    private InputAction atkL;
    private InputAction atkH;
    private InputAction block;

    protected void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInput>();
        move = input.currentActionMap.FindAction("Move");
        atkL = input.currentActionMap.FindAction("Light Attack");
        atkH = input.currentActionMap.FindAction("Heavy Attack");
        block = input.currentActionMap.FindAction("Block");
    }

    void OnMove()
    {
        Direction = move.ReadValue<Vector2>();
    }

    void OnLightAttack()
    {
        AtkLight = atkL.IsPressed();
    }

    void OnHeavyAttack()
    {
        AtkHeavy = atkH.IsPressed();
    }

    void OnBlock()
    {
        Blocking = block.IsPressed();
    }

}
