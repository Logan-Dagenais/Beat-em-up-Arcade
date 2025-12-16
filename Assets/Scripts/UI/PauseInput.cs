using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInput : MonoBehaviour
{
    private InputAction Pause;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject OnscreenUI;
    [SerializeField] private GameObject ControlsUI;

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
        //if (!MenuSelection.CanPause)
        //{
        //    return;
        //}

        if (PlayerScript.GameOver)
        {
            return;
        }

        Time.timeScale = 0;
        OnscreenUI.SetActive(PauseUI.activeSelf);
        PauseUI.SetActive(!PauseUI.activeSelf);
        if (ControlsUI.activeSelf)
        {
            ControlsUI.SetActive(false);
        }
        
    }
}
