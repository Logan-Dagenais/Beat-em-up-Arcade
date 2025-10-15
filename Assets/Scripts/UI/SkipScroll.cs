using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipScroll : MonoBehaviour
{
    public GameObject skipButton;
    public float timer;
    private InputAction Select;
    private InputAction Pause;

    private void Awake()
    {
        //menuState = SceneManager.GetActiveScene().buildIndex == 0 ? MenuState.MainMenu : MenuState.InGame;

        Select = InputSystem.actions.FindAction("Select");
        Pause = InputSystem.actions.FindAction("Pause");
        Select.performed += OnSelect;
        Pause.performed += OnSelect;

    }

    private void OnSelect(InputAction.CallbackContext ctx)
    {
        MoveOn();
        
    }

    private void OnDestroy()
    {
        Select.performed -= OnSelect;
        Pause.performed -= OnSelect;
    }

    public void MoveOn()
    {
        SceneManager.LoadScene(2);
    }
}
