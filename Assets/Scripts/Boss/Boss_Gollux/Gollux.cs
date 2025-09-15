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
    private Gollux_SkillManager skillManager;


    protected override void Awake()
    {
        base.Awake();

        moveCommand = new Gollux_MoveCommand(this, EParamenter_Boss.isMove.ToString(), 2f);
        rockDropCommand = new Gollux_RockDropCommand(this, EParamenter_Boss.isRockDrop.ToString());
        normalAttackCommand = new Gollux_NormalAttackCommand(this, EParamenter_Boss.isNormalAttack.ToString());
        summonCommand = new Gollux_SummonCommand(this, EParamenter_Boss.isSummon.ToString());
        healCommand = new Gollux_HealCommand(this, EParamenter_Boss.isHeal.ToString());

        skillManager = GetComponent<Gollux_SkillManager>();
    }

    protected override void Update()
    {
        base.Update();

        HandleFlip();
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
        skillManager.rockDrop.Perform();
    }

    public void SkillSummon()
    {
        skillManager.summon.Perform();
    }

    public void SkillSummonDetroy()
    {
        skillManager.summon.DismissAllSummon();
    }

    public void SkillSummonHeal()
    {
        skillManager.summon.Heal();
    }

    public void Idle()
    {
        rb.linearVelocityX = 0;
    }

    public void BeFreezed()
    {
        sr.color = Color.blue;
    }

    public void OutFreezed()
    {
        sr.color = Color.white;
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

    private void HandleFlip()
    {
        if (detectTarget == null)
            return;

        if (transform.position.x < detectTarget.transform.position.x && faceDir != 1)
            Flip();

        if (transform.position.x > detectTarget.transform.position.x && faceDir != -1)
            Flip();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Close range attack distance
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(closeAttackDistance * faceDir, 0));
    }
}
