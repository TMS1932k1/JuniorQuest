using System.Collections;
using UnityEngine;

public class Gollux : Boss
{
    [Header("Move Details")]
    [SerializeField] protected float moveSpeed;
    private Coroutine moveCoroutine;


    [Header("Attack Details")]
    public float closeAttackDistance;


    // Components
    private Gollux_SkillManager skillManager;


    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponent<Gollux_SkillManager>();
    }

    protected override void Update()
    {
        base.Update();

        HandleFlip();
    }

    public void Move()
    {
        // Animation
        anim.Play(EAnim_Gollux.Gollux_Move.ToString());

        // Logic Move
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveCo());
    }

    private IEnumerator MoveCo()
    {
        while (GetDisToTarget() > 0.1f)
        {
            rb.linearVelocityX = moveSpeed * GetDirToTarget();
            yield return null;
        }
    }

    /// <summary>
    /// That is basic state to wait new command
    ///  - Need stop (moveCoroutine) of (MoveCommand)
    /// </summary>
    public void Idle()
    {
        // Animation
        anim.Play(EAnim_Gollux.Gollux_Idle.ToString());

        // Stop Move
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    public void SkillAttack1()
    {
        // Animation
        anim.Play(EAnim_Gollux.Gollux_Attack1.ToString());

        // Logic Attack 1
        skillManager.PerformSkillAttack1();
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
