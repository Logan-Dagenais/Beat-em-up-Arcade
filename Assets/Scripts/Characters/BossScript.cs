using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : EnemyScript
{
    [SerializeField] private Slider healthbar;
    [SerializeField] private LightningAnim lAnim;
    [SerializeField] private SpriteRenderer sr;

    protected void Start()
    {
        lAnim = transform.GetChild(2).GetComponent<LightningAnim>();
        sr = GetComponent<SpriteRenderer>();
        healthbar = GetComponentInChildren<Slider>();
        healthbar.maxValue = MaxHealth;
        healthbar.value = Health;
    }

    private void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        healthbar.value = Health;
        StartCoroutine(HitFlash());
    }

    protected override void SetBehaviorStateList()
    {
        BehaviorStateMach.StateList = new()
        {
            {(int)BehaviorStates.DEFENSIVE,
            new DefenseJumpAltState(this)},

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

    IEnumerator HitFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.05f);
        sr.color = Color.white;
        StopCoroutine(HitFlash());
    }

    public void LightningEffect()
    {
        lAnim.PlayEffect();
    }
}
