using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : CharacterScript
{
    // private PlayerInput input;
    [Header("player traits")]
    private InputAction move;
    private InputAction atkL;
    private InputAction atkH;
    private InputAction block;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider guardMeter;
    static public bool GameOver;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject runFX;

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
        EnemySpawner.TotalEnemyCount = 0;
    }

    private void Update()
    {
        guardMeter.value = GuardIntegrity;
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

    /*
    public override void RecoverGuard()
    {
        base.RecoverGuard();
        guardMeter.value = GuardIntegrity;
    }
    */

    public override void SwitchSpriteDirection(bool left)
    {
        base.SwitchSpriteDirection(left);

        runFX.transform.rotation = Facingleft ? Quaternion.Euler(-30f, 90f, 0f) : Quaternion.Euler(-30f, -90f, 0f);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    void OnLightAttack(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
            return;

        AtkLight = atkL.IsPressed();
    }

    void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
            return;

        AtkHeavy = atkH.IsPressed();
    }

    void OnBlock(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
            return;

        Blocking = block.IsPressed();
    }

    public override void DeadState()
    {
        MenuSelection.CanPause = false;

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
        AudioListener.volume = 0f;

        GameOver = true;

        StopCoroutine(PlayerDeath());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int healing = 30;
        if(collision.CompareTag("healthPickup") && Health != MaxHealth)
        {
            if (Health + healing >= MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += healing;
            }
            HealingSound.Play();
            healthBar.value = Health;
            collision.gameObject.SetActive(false);
            return;
        }
        base.OnTriggerEnter2D(collision);
    }

}
