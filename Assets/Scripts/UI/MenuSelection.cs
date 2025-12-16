using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{

    [SerializeField] SelectionAnim SAS; //unused cause unity sucks
    [SerializeField] GameObject ControlsUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameObject GeneralPauseUI;
    [SerializeField] GameObject OnscreenUI;
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
        MainMenu,
        Options,
        Controls
    }

    public static bool CanPause;

    [SerializeField] private int menuIndex = 0;
    [SerializeField] private List<Option> menuOptions;
    //[SerializeField] private List<Transform> menuTransforms;
    [SerializeField] private List<Action> menuActions = new(); 

    [SerializeField] private RectTransform Cursor;
    [SerializeField] private Vector2 cursorOffset;

    [SerializeField] private GameObject CreditsUI;
    private bool subTabOn;

    private InputAction Navigate;
    private InputAction Select;

    private void Awake()
    {
        //menuState = SceneManager.GetActiveScene().buildIndex == 0 ? MenuState.MainMenu : MenuState.InGame;
        AudioListener.volume = 1f;
        Navigate = InputSystem.actions.FindAction("Move");
        Select = InputSystem.actions.FindAction("Select");

        Navigate.performed += OnNavigate;
        Select.performed += OnSelect;
    }

    private void OnEnable()
    {
        // Debug.Log("active");
        Time.timeScale = 0;
        menuIndex = 0;
        Cursor.anchoredPosition = menuOptions[menuIndex].OptionTrans.anchoredPosition + cursorOffset;
        if(SAS)
            SAS.MenuSwitch();
    }


    private void OnDisable()
    {
        // Debug.Log("inactive");
        if (subTabOn)
        {
            OpenControls();
        }

        Time.timeScale = 1;
       if(SAS)
            SAS.MenuClosed();
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

                case (OptionName.Options):
                    menuActions.Add(() => OpenOptions());
                    break;

                case (OptionName.Controls):
                    menuActions.Add(() => OpenControls());
                    break;
            }
        }

    }

    private void OpenControls()
    {
        ControlsUI.SetActive(!ControlsUI.activeSelf);
        subTabOn = ControlsUI.activeSelf;
    }
    private void CloseWindows()
    {
        ControlsUI.SetActive(false);
        OptionsUI.SetActive(false);
        GeneralPauseUI.SetActive(true);
    }

    private void OpenOptions()
    {
        ControlsUI.SetActive(false);
        OptionsUI.SetActive(true);
        GeneralPauseUI.SetActive(false);
    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        PlayerScript.GameOver = false;
        CanPause = true;
        SceneManager.LoadScene(1);
    }

    //  this is reliant on the structure of a pause prefab
    //  will definitely break if somehow called by a different menu type
    private void ContinueGame()
    {
        Debug.Log("resume game");
        transform.parent.gameObject.SetActive(false);
        OnscreenUI.SetActive(true);
    }

    private void Credits()
    {
        CreditsUI.SetActive(!CreditsUI.activeSelf);
        subTabOn = CreditsUI.activeSelf;
        Debug.Log("Display Credits");
    }

    private void RestartLevel()
    {
        AudioListener.volume = 1f;
        PlayerScript.GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnMainMenu()
    {
        CanPause = false;
        SceneManager.LoadScene(0);
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        bool subTabCheck = (CreditsUI || ControlsUI) && subTabOn;

        if (subTabCheck || !transform.parent.gameObject.activeSelf)
        {
            return;
        }

        menuIndex = Mathf.Clamp(menuIndex - (int)ctx.ReadValue<Vector2>().y, 0, menuOptions.Count-1);

        if(menuIndex < menuOptions.Count)
            Cursor.anchoredPosition = menuOptions[menuIndex].OptionTrans.anchoredPosition + cursorOffset;
            //SAS.StopAnimation();
        if(SAS)
            SAS.MenuSwitch();
    }

    private void OnSelect(InputAction.CallbackContext ctx)
    {
        if (!transform.parent.gameObject.activeSelf)
        {
            return;
        }
        //AudioListener.volume = 1f;
        menuActions[menuIndex].Invoke();
    }
}
