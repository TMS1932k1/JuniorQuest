using System.Collections;
using UnityEngine;

public class Gollux : Boss
{
    [Header("Move Details")]
    [SerializeField] protected float moveSpeed;
    private Coroutine moveCoroutine;


    [Header("Attack Details")]
    public float closeAttackDistance;


    // Commands
    public Gollux_MoveCommand moveCommand { get; private set; }
    public Gollux_RockDropCommand rockDropCommand { get; private set; }
    public Gollux_NormalAttackCommand normalAttackCommand { get; private set; }
    public Gollux_SummonCommand summonCommand { get; private set; }
    public Gollux_HealCommand healCommand { get; private set; }


    // Components
    private Gollux_SkillManager golluxSkillManager;
    private Boss_VFX bossVFX;


    protected override void Awake()
    {
        base.Awake();

        moveCommand = new Gollux_MoveCommand(this, BossAnimationStrings.moveAnim, 2f);
        rockDropCommand = new Gollux_RockDropCommand(this, GolluxAnimationStrings.rockDropAnim);
        normalAttackCommand = new Gollux_NormalAttackCommand(this, GolluxAnimationStrings.normalAttackAnim);
        summonCommand = new Gollux_SummonCommand(this, GolluxAnimationStrings.summonAnim);
        healCommand = new Gollux_HealCommand(this, GolluxAnimationStrings.healAnim);

        golluxSkillManager = GetComponent<Gollux_SkillManager>();
        bossVFX = GetComponent<Boss_VFX>();
    }

    public void Move()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveCo());
    }

    private IEnumerator MoveCo()
    {
        while (GetDisToTarget() > 0.5f)
        {
            rb.linearVelocityX = moveSpeed * GetDirToTarget();
            yield return null;
        }
    }

    public void StopMove()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    public void SkillRockDrop()
    {
        golluxSkillManager.rockDrop.Perform();
    }

    public void SkillSummon()
    {
        golluxSkillManager.summon.Perform();
    }

    public void SkillSummonDetroy()
    {
        golluxSkillManager.summon.DismissAllSummon();
    }

    public void SkillSummonHeal()
    {
        golluxSkillManager.summon.Heal();
    }

    public void Idle()
    {
        rb.linearVelocityX = 0;
    }

    public void Death()
    {
        bossVFX.ResetVFX();
        bossVFX.PlayDeathVFX();

        golluxSkillManager.summon.DismissAllSummon();
    }

    /// <summary>
    /// Get distance to target (-1 = no target)
    /// </summary>
    /// <returns></returns>
    public float GetDisToTarget()
    {
        if (detectTarget == null)
            return -1;

        return Mathf.Abs(transform.position.x - detectTarget.transform.position.x);
    }

    private int GetDirToTarget()
    {
        if (detectTarget == null)
            return 0;

        return transform.position.x > detectTarget.transform.position.x ? -1 : 1;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Close range attack distance
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(closeAttackDistance * faceDir, 0));
    }
}
