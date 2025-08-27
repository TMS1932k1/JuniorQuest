using System;
using UnityEngine;

public class Enemy : Entity, ICanCounter
{
    public static event Action<float> OnEnemyDeath;


    [Header("Move details")]
    public float moveSpeed;


    [Header("Attack details")]
    [SerializeField] public float distanceToAttack;
    [SerializeField] protected float distanceToDetectPlayer;
    [SerializeField] private LayerMask whatIsPlayer;
    public bool isAttack { get; private set; }
    public RaycastHit2D playerDetect { get; private set; }
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
    public Enemy_PlayerDetectedState playerDetectedState;
    public Enemy_DeathState deathState;
    public Enemy_StunnedState stunnedState;


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

    public override void OnDead()
    {
        base.OnDead();

        stateMachine.ChangeState(deathState);
        OnEnemyDeath.Invoke(stat.GetXp());
    }

    protected override void HandleCollisions()
    {
        base.HandleCollisions();

        isAttack = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToAttack, whatIsPlayer);
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
    /// if not detect or have obstacle(whatIsGround) then return (default)
    /// </summary>
    /// <returns></returns>
    public RaycastHit2D DetectPlayer()
    {
        playerDetect = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToDetectPlayer, whatIsPlayer | whatIsWall);

        if (playerDetect.collider == null || playerDetect.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return playerDetect;
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
