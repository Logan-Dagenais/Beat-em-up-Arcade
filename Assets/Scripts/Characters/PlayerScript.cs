using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : CharacterScript
{
    private PlayerInput input;

    private InputAction move;
    private InputAction atkL;
    private InputAction atkH;
    private InputAction block;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider guardMeter;
    [SerializeField] private GameObject gameOverScreen;

    protected void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInput>();
        move = input.currentActionMap.FindAction("Move");
        atkL = input.currentActionMap.FindAction("Light Attack");
        atkH = input.currentActionMap.FindAction("Heavy Attack");
        block = input.currentActionMap.FindAction("Block");
    }

    private void Start()
    {
        healthBar.maxValue = MaxHealth;
        healthBar.value = MaxHealth;
        guardMeter.maxValue = MaxGuardIntegrity;
        guardMeter.value = MaxGuardIntegrity;
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        healthBar.value = Health;
    }

    public override void RecoverGuard()
    {
        base.RecoverGuard();
        guardMeter.value = GuardIntegrity;
    }

    void OnMove()
    {
        Direction = move.ReadValue<Vector2>();
    }

    void OnLightAttack()
    {
        AtkLight = atkL.IsPressed();
    }

    void OnHeavyAttack()
    {
        AtkHeavy = atkH.IsPressed();
    }

    void OnBlock()
    {
        Blocking = block.IsPressed();
    }

    public override void DeadState()
    {
        StartCoroutine(PlayerDeath());
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(2);
        Destroy(StateMach);
        Velocity.y = 0;
        spriteRender.enabled = false;
        yield return new WaitForSeconds(1);
        gameOverScreen.SetActive(true);

        StopCoroutine(PlayerDeath());
    }

}
