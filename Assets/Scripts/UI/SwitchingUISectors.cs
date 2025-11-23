using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingUISectors : MonoBehaviour
{
    public GameObject General;
    public GameObject Movement;
    public GameObject Combat;
    public GameObject Advanced;

    public GameObject GeneralActive;
    public GameObject MovementActive;
    public GameObject CombatActive;
    public GameObject AdvancedActive;
    [SerializeField] GameObject ControlsUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameObject GeneralPauseUI;

    public void OpenGeneral()
    {
        GeneralActive.SetActive(true);
        MovementActive.SetActive(false);
        CombatActive.SetActive(false);
        AdvancedActive.SetActive(false);
    }

    public void OpenMovement()
    {
        MovementActive.SetActive(true);
        CombatActive.SetActive(false);
        AdvancedActive.SetActive(false);
        GeneralActive.SetActive(false);
    }

    public void OpenCombat()
    {
        CombatActive.SetActive(true);
        GeneralActive.SetActive(false);
        MovementActive.SetActive(false);
        AdvancedActive.SetActive(false);
    }

    public void OpenAdvanced()
    {
        AdvancedActive.SetActive(true);
        GeneralActive.SetActive(false);
        CombatActive.SetActive(false);
        MovementActive.SetActive (false);
    }

    public void CloseAll()
    {
        OpenGeneral();
        this.gameObject.SetActive(false);
    }
    public void CloseWindows()
    {
        ControlsUI.SetActive(false);
        OptionsUI.SetActive(false);
        GeneralPauseUI.SetActive(true);
    }
}
