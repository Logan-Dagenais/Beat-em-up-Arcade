using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject CreditsUI;
    public GameObject SceneSelectUI;
    public GameObject QuitCheckUI;

    //public PlayerInput MenuActions;
    public InputAction SceneSelect;
    public InputAction PanicButton;

    public string Warning1;
    public string Warning2;
    public TMP_Text WarningBox;


    private void Start()
    {
        //MenuActions.currentActionMap.Enable();
        SceneSelect = InputSystem.actions.FindAction("OpenSceneSelect");
        PanicButton = InputSystem.actions.FindAction("PanicButton");
        SceneSelect.started += Handle_OpenSceneSelection;
        PanicButton.started += Handle_PanicButton;
    }

    private void Handle_PanicButton(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private void Handle_OpenSceneSelection(InputAction.CallbackContext context)
    {
        if (!SceneSelectUI.activeInHierarchy)
        {
            SceneSelectUI.SetActive(true);
        }

    }

    private void OnDestroy()
    {
        SceneSelect.started -= Handle_OpenSceneSelection;
        PanicButton.started -= Handle_PanicButton;
    }

    /* This function is called through each of the SceneSelection buttons in our main menu.
     * This will automatically open one of our assigned scenes, depending on which button you select.
     * In order for this to work, it simply needs the correct name added to the button call, the
     * order to scene assignment will not effect this, as it automatically calls the correct ID, 
     * regardless of where that scene is in the load order.
     * 
     * 
     * To add a new scene:
     * Create a new scene object
     * Open this scene object
     * Select "File", the top left Unity category
     * Select Build Profiles
     * Click "Add Open Scenes"
     * Make sure all boxes are checked
     * Select lowest ID unused button
     * Add MenuManager call in said button
     * Type the name of your scene exactly as seen in the Build Profiles
     * Change the text of that button to the name of the scene as seen in Build Profiles
     */
    public void SelectScene(string select)
    {
        if (select.Contains("Unused"))
        {
            StartCoroutine(WarningSceneSelect());
        }
        else
        {
            SceneSelectUI = null;
            SceneSelect.started -= Handle_OpenSceneSelection;
            print("Opening" + select);
            SceneManager.LoadScene(select);
        }

    }


    // Basic calls for opening and closing UI sections in this Scene
    public void SceneSelectUIButton()
    {
        SceneSelectUI.SetActive(true);
    }
    public void UndoSceneSelectUIButton()
    {
        SceneSelectUI.SetActive(false);
    }
    public void CreditsUIButton()
    {
        CreditsUI.SetActive(true);
    }
    public void UndoCreditsUIButton()
    {
        CreditsUI.SetActive(false);
    }
    public void QuitCheckButton()
    {
        QuitCheckUI.SetActive(true);
    }
    public void UndoQuitCheckButton()
    {
        QuitCheckUI.SetActive(false);
    }

    public void DoQuit()
    {
        Application.Quit();
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu_Proto");
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public IEnumerator WarningSceneSelect()
    {
        WarningBox.text = Warning1;
        yield return new WaitForSeconds(2);
        WarningBox.text = Warning2;
        yield return new WaitForSeconds(7);
        WarningBox.text = " ";
    }

}
