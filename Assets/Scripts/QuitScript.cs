using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitScript : MonoBehaviour
{
    private InputAction Quit;
    private void Start()
    {
        Quit = InputSystem.actions.FindAction("Quit");
        Quit.performed += QuitGame;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void QuitGame(InputAction.CallbackContext context)
    {
        print("Quit game");
        Application.Quit();
    }
}
