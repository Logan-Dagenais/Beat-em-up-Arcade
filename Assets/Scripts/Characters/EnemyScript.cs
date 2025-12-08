using UnityEngine;
using UnityEngine.Audio;

public class EnemyScript : CharacterScript
{
    [Header("enemy traits")]
    public StateMachine BehaviorStateMach;

    public Transform PlayerTransform;
    public PlayerScript PlayerScript;
    public int PlayerState => PlayerScript.StateMach.CurrentState;

    //  changes during combat depending on mode
    public float CombatRangeDistance;

    //  combat attributes for behavior, stays the same
    public float EngagementRange;
    public float AttackRange;
    //  for if the target is not directly the player (mostly for ranged enemies)
    public float TargetOffset;

    public float Target => PlayerToLeft ? PlayerTransform.position.x + TargetOffset : PlayerTransform.position.x - TargetOffset;

    public float CombatRangeMax => Target + CombatRangeDistance;
    public float CombatRangeMin => Target - CombatRangeDistance;

    public bool PlayerToLeft => PlayerTransform.position.x < transform.position.x;
    public bool InCombatRange => transform.position.x <= CombatRangeMax &&
                                 transform.position.x >= CombatRangeMin;

    public float AgressionTimer;
    public float AggresionTimerMin;
    public float AggresionTimerMax;

    public float DefenseTimer;
    public float DefenseTimerMin;
    public float DefenseTimerMax;

    [Range(0, 10)] public float BlockChance;

    [Range(0, 10)] public float CrouchChance;
    [Range(0, 10)] public float JumpChance;

    public bool PlayerContact;

    [SerializeField] private bool dropsItem;
    [SerializeField] private GameObject coffeeCup;

    protected void Awake()
    {
        base.Awake();
        CombatRangeDistance = EngagementRange;
        PlayerScript = FindAnyObjectByType<PlayerScript>();
        PlayerTransform = PlayerScript.transform;

        BehaviorStateMach = gameObject.AddComponent<StateMachine>();

        WalkBackwards = true;

        SetBehaviorStateList();
    }

    //  overridable so that we can change enemies states if needed
    protected virtual void SetBehaviorStateList()
    {
        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new DefenseState(this)},

            {(int)BehaviorStates.OFFENSIVE,
            new OffenseState(this)},

            {(int)BehaviorStates.PUSH,
            new PushState(this)},

            {(int)BehaviorStates.CHASE,
            new ChaseState(this)},

            {(int)BehaviorStates.JUMP,
            new JumpOffenseState(this)}
        };
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //  checking for player layer
        if (collision.gameObject.layer == 0 && collision.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name + " collided");
            PlayerContact = true;
        }

        base.OnTriggerEnter2D (collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //  checks if trigger collided with main player collider instead of hurtbox or hitbox
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 0)
        {
            Debug.Log(collision.gameObject.name + " collided");
            PlayerContact = true;
        }
    }

    bool hasSpawned = false;
    public override void DeadState()
    {
        if (dropsItem == true && hasSpawned == false)
        {
            Instantiate(coffeeCup, transform.position, Quaternion.identity);
            hasSpawned = true;
        }
        /* put the enemy pick up stuff here */
        base.DeadState();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //  checks if trigger collided with main player collider instead of hurtbox or hitbox
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 0)
        {
            PlayerContact = false;
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        //  checks if trigger collided with main player collider instead of hurtbox or hitbox
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 0)
        {
            Debug.Log(collision.gameObject.name+" collided");
            PlayerContact = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //  checks if trigger collided with main player collider instead of hurtbox or hitbox
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 0)
        {
            PlayerContact = false;
        }
    }
    */

    //  probably would want to move this variable and method to a subclass.
    //  we need a way to customize offset based on animation or projectile enemy type.
    //  unfortunately the event system won't let us input a vector as a parameter.
    [SerializeField] private Vector2 projectileOffset;
    public void SpawnProjectile(GameObject projectile)
    {
        if (!projectile)
        {
            return;
        }

        ShootSound();
        ProjectileScript proj = Instantiate(projectile,
                                            (Vector2)transform.position + projectileOffset,
                                            Quaternion.identity)
                                            .GetComponent<ProjectileScript>();
        proj.Direction.x = Facingleft ? -1 : 1;
        Destroy(proj.gameObject, proj.lifeTime);
    }

    //  probably not good structurally but will work for now
    public AudioSource shotSound;
    public void ShootSound()
    {
        if (!shotSound)
        {
            return;
        }

        shotSound.Play();
    }

    private void OnDestroy()
    {
        EnemySpawner.TotalEnemyCount--;
    }

    private void Start()
    {
        SwitchSpriteDirection(PlayerToLeft);
    }

}
