using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private float ScrollSpeed;
    //  limits how far the text scrolls downwards
    [SerializeField] private float InitialPosition;
    [SerializeField] private float EndGoal;
    [SerializeField] private SkipScroll Skip;
    private InputAction move;
    private float Direction;

    private void Awake()
    {
        move = InputSystem.actions.FindAction("Move");

        move.performed += OnMove;
        move.canceled += OnMove;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>().y;
    }

    private void OnDestroy()
    {
        move.performed -= OnMove;
        move.canceled -= OnMove;
    }

    private void Update()
    {
        if (Direction != 0 && transform.position.y >= InitialPosition)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (ScrollSpeed * -Direction * 5 * Time.deltaTime));
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (ScrollSpeed * Time.deltaTime));
        }

        if(gameObject.transform.position.y >= EndGoal)
        {
            Skip.MoveOn();
        }
    }
}
