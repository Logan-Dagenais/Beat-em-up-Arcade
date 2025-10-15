using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipScroll : MonoBehaviour
{
    public GameObject skipButton;
    public float timer;
    private InputAction Select;

    private void Awake()
    {
        //menuState = SceneManager.GetActiveScene().buildIndex == 0 ? MenuState.MainMenu : MenuState.InGame;

        Select = InputSystem.actions.FindAction("Select");
        Select.performed += OnSelect;
    }

    private void OnSelect(InputAction.CallbackContext ctx)
    {
        MoveOn();
        
    }

    public void MoveOn()
    {
        SceneManager.LoadScene(2);
    }
}
