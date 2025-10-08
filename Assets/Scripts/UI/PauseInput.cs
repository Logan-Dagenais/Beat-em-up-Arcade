using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInput : MonoBehaviour
{
    private InputAction Pause;
    [SerializeField] private GameObject PauseUI;

    private void Start()
    {
        PauseUI = transform.GetChild(0).gameObject;

        Pause = InputSystem.actions.FindAction("Pause");

        Pause.performed += OnPause;
    }

    private void OnDestroy()
    {
        Pause.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (MenuSelection.menuState != MenuSelection.MenuState.InGame)
        {
            return;
        }

        Time.timeScale = 0;
        PauseUI.SetActive(!PauseUI.activeSelf);
    }
}
