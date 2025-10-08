using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    [Serializable]
    public struct Option
    {
        [SerializeField] public RectTransform OptionTrans;
        [SerializeField] public OptionName menuName;
        [SerializeField] public Action MenuAction;
    }

    [Serializable]
    public enum OptionName
    {
        Start,
        Continue,
        Credits,
        Quit,
        Reset,
        MainMenu
    }

    [SerializeField] private int menuIndex = 0;
    [SerializeField] private List<Option> menuOptions;
    //[SerializeField] private List<Transform> menuTransforms;
    [SerializeField] private List<Action> menuActions = new(); 

    [SerializeField] private RectTransform Cursor;
    [SerializeField] private Vector2 cursorOffset;

    [SerializeField] private GameObject CreditsUI;

    private InputAction Navigate;
    private InputAction Select;

    private void Awake()
    {
        Navigate = InputSystem.actions.FindAction("Move");
        Select = InputSystem.actions.FindAction("Select");

        Navigate.performed += OnNavigate;
        Select.performed += OnSelect;
    }

    private void OnEnable()
    {
        Debug.Log("active");
        menuIndex = 0;
        Cursor.anchoredPosition = menuOptions[menuIndex].OptionTrans.anchoredPosition + cursorOffset;
    }

    private void OnDisable()
    {
        Debug.Log("inactive");
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
    }

    private void OnDestroy()
    {
        Navigate.performed -= OnNavigate;
        Select.performed -= OnSelect;
    }

    void Start()
    {
        /*
        for (int i = 0; i < transform.childCount; i++)
        {
            menuTransforms.Add(transform.GetChild(i));
        }
        */

        for (int i = 0; i < menuOptions.Count; i++)
        {
            switch (menuOptions[i].menuName)
            {
                case (OptionName.Start):
                    menuActions.Add(() => StartGame());
                    break;

                case (OptionName.Credits):
                    menuActions.Add(() => Credits());
                    break;

                case (OptionName.Quit):
                    menuActions.Add(() => Quit());
                    break;

                case (OptionName.Continue):
                    menuActions.Add(() => ContinueGame());
                    break;

                case (OptionName.Reset):
                    menuActions.Add(() => RestartLevel());
                    break;

                case (OptionName.MainMenu):
                    menuActions.Add(() => ReturnMainMenu());
                    break;
            }
        }

    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);
    }

    //  this is reliant on the structure of a pause prefab
    //  will definitely break if somehow called by a different menu type
    private void ContinueGame()
    {
        Debug.Log("resume game");
        transform.parent.gameObject.SetActive(false);
    }

    private void Credits()
    {
        CreditsUI.SetActive(!CreditsUI.activeSelf);
        Debug.Log("Display Credits");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        if (CreditsUI && CreditsUI.activeSelf)
        {
            return;
        }

        menuIndex = Mathf.Clamp(menuIndex - (int)ctx.ReadValue<Vector2>().y, 0, menuOptions.Count-1);

        if(menuIndex < menuOptions.Count)
            Cursor.anchoredPosition = menuOptions[menuIndex].OptionTrans.anchoredPosition + cursorOffset;
    }

    private void OnSelect(InputAction.CallbackContext ctx)
    {
        menuActions[menuIndex].Invoke();
    }
}
