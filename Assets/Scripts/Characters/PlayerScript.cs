using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : CharacterScript
{
    // private PlayerInput input;

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
        //input = GetComponent<PlayerInput>();
        move = InputSystem.actions.FindAction("Move");
        atkL = InputSystem.actions.FindAction("Light Attack");
        atkH = InputSystem.actions.FindAction("Heavy Attack");
        block = InputSystem.actions.FindAction("Block");

        move.performed += OnMove;
        move.canceled += OnMove;

        atkL.performed += OnLightAttack;
        atkH.performed += OnHeavyAttack;
        block.performed += OnBlock;
    }

    private void Start()
    {
        healthBar.maxValue = MaxHealth;
        healthBar.value = MaxHealth;
        guardMeter.maxValue = MaxGuardIntegrity;
        guardMeter.value = MaxGuardIntegrity;
    }

    private void OnDestroy()
    {
        move.performed -= OnMove;
        move.canceled -= OnMove;

        atkL.performed -= OnLightAttack;
        atkH.performed -= OnHeavyAttack;
        block.performed -= OnBlock;
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

    void OnMove(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    void OnLightAttack(InputAction.CallbackContext context)
    {
        AtkLight = atkL.IsPressed();
    }

    void OnHeavyAttack(InputAction.CallbackContext context)
    {
        AtkHeavy = atkH.IsPressed();
    }

    void OnBlock(InputAction.CallbackContext context)
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
