using System;
using System.Collections;
using UnityEngine;

public class Enemy : Entity, ICanCounter
{
    public static event Action<float> OnEnemyDeath;


    [Header("Move details")]
    public float moveSpeed;


    [Header("Attack details")]
    [SerializeField] public float distanceToAttack;
    [SerializeField] protected float distanceToDetectPlayer;
    [SerializeField] protected LayerMask whatIsPlayer;
    public bool isAttack { get; protected set; }
    public Collider2D playerDetect { get; protected set; }
    public float moveDetectedSpeed;
    public float durationDetect;


    [Header("Stun details")]
    public Vector2 stunnedVelocity;
    public float stunnedDuration;
    public bool canStunned;


    // States
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_DetectedState detectedState;
    public Enemy_DeathState deathState;
    public Enemy_StunnedState stunnedState;
    public Enemy_FreezedState freezedState;


    private Entity_Stat stat;


    protected override void Awake()
    {
        base.Awake();

        stat = GetComponent<Entity_Stat>();
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnDead()
    {
        base.OnDead();

        stateMachine.ChangeState(deathState);
        OnEnemyDeath.Invoke(stat.GetXp());
    }

    public void HandleCounter()
    {
        if (canStunned)
        {
            stateMachine.ChangeState(stunnedState);
        }
    }

    public bool GetCanCounter => canStunned;

    public Transform GetTransform => transform;

    public void HandlePlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }

    public float GetAnimSpeedMutiplier()
    {
        return moveDetectedSpeed / moveSpeed;
    }

    /// <summary>
    /// Detect player with (distanceToDetectPlayer)
    /// if not detect then return (null)
    /// </summary>
    /// <returns></returns>
    public virtual Collider2D DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToDetectPlayer, whatIsPlayer | whatIsWall);

        playerDetect = hit.collider;
        if (playerDetect == null || playerDetect.gameObject.layer != LayerMask.NameToLayer("Player"))
            return null;

        return playerDetect;
    }

    public override void BeFreezed(float duration)
    {
        base.BeFreezed(duration);

        stateMachine.ChangeState(freezedState);
        Invoke(nameof(ExitFreezed), duration); // Exit freezed after duration
    }

    public override void ExitFreezed()
    {
        base.ExitFreezed();

        stateMachine.ChangeState(detectedState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Line detect player to attack
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(distanceToAttack * faceDir, 0, 0));

        // Line detect player to folow
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(distanceToDetectPlayer * faceDir, 0, 0));
    }
}
