using UnityEngine;

public class EnemyScript : CharacterScript
{
    public StateMachine BehaviorStateMach;

    public Transform PlayerTransform;
    public PlayerScript PlayerScript;
    public int PlayerState => (int)PlayerScript.StateMach.CurrentState;

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

    protected void Awake()
    {
        base.Awake();
        CombatRangeDistance = EngagementRange;
        PlayerScript = FindAnyObjectByType<PlayerScript>();
        PlayerTransform = PlayerScript.transform;

        BehaviorStateMach = gameObject.AddComponent<StateMachine>();

        WalkBackwards = true;

        //  behavior states
        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new DefenseState(this)},

            {(int)BehaviorStates.OFFENSIVE,
            new OffenseState(this)},

            {(int)BehaviorStates.CHASE,
            new ChaseState(this)}
        };
    }

    //  probably would want to move this variable and method to a subclass.
    //  we need a way to customize offset based on animation or projectile enemy type.
    //  unfortunately the event system won't let us input a vector as a parameter.
    [SerializeField] private Vector2 projectileOffset;
    public void SpawnProjectile(GameObject projectile)
    {
        if (projectile != null)
        {
            GameObject proj = Instantiate(projectile, (Vector2)transform.position + projectileOffset, Quaternion.identity);
            proj.GetComponent<ProjectileScript>().Direction.x = Facingleft ? -1 : 1;
            Destroy(proj, 10);
        }
    }

    private void Start()
    {
        SwitchSpriteDirection(PlayerToLeft);
    }
}
