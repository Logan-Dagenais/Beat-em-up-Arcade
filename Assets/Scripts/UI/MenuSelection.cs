using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    [SerializeField] private int menuIndex = 0;
    [SerializeField] private List<Transform> menuTransforms;
    [SerializeField] private List<Action> menuActions = new(); 
    [SerializeField] private Transform Cursor;
    [SerializeField] private GameObject CreditsUI;
    [SerializeField] private Vector2 cursorOffset;

    private InputAction Navigate;
    private InputAction Select;

    private void Awake()
    {
        Navigate = InputSystem.actions.FindAction("Move");
        Select = InputSystem.actions.FindAction("Select");

        Navigate.performed += OnNavigate;
        Select.performed += OnSelect;
    }

    private void OnDestroy()
    {
        Navigate.performed -= OnNavigate;
        Select.performed -= OnSelect;
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            menuTransforms.Add(transform.GetChild(i));
        }

        menuActions.Add(() => StartGame());
        menuActions.Add(() => Credits());
        menuActions.Add(() => Quit());
    }

    void Update()
    {
        
    }

    private void MenuAction()
    {

    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);
    }

    private void Credits()
    {
        CreditsUI.SetActive(!CreditsUI.activeSelf);
        Debug.Log("Display Credits");
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        if (CreditsUI.activeSelf)
        {
            return;
        }

        menuIndex = Mathf.Clamp(menuIndex - (int)ctx.ReadValue<Vector2>().y, 0, menuTransforms.Count-1);

        if(menuIndex < menuTransforms.Count)
            Cursor.position = (Vector2)menuTransforms[menuIndex].position + cursorOffset;
    }

    private void OnSelect(InputAction.CallbackContext ctx)
    {
        menuActions[menuIndex].Invoke();
    }
}
