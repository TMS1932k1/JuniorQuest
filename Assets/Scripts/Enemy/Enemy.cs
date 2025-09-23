using System;
using UnityEngine;

public class Enemy : Entity, ICanCounter, ISaveable
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


    private Entity_Stat entityStat;
    public Enemy_Health enemyHealth { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        entityStat = GetComponent<Entity_Stat>();
        enemyHealth = GetComponent<Enemy_Health>();
    }

    protected virtual void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    protected virtual void OnDisable()
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
        OnEnemyDeath.Invoke(entityStat.GetXp());
    }

    public void HandleCounter()
    {
        if (canStunned)
        {
            stateMachine.ChangeState(stunnedState);
        }
    }

    public bool GetCanCounter => canStunned;

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
        if (playerDetect == null || playerDetect.gameObject.layer != LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER))
            return null;

        return playerDetect;
    }

    /// <summary>
    /// Change to freezedstate 
    /// </summary>
    /// <param name="duration">Time freeze</param>
    public override void BeFreezed(float duration)
    {
        stateMachine.ChangeState(freezedState);
    }

    public override void ExitFreezed()
    {
        stateMachine.ChangeState(detectedState);
    }

    public virtual void SaveData(ref GameData gameData)
    {
        if (!gameData.entities.ContainsKey(uniqueId))
        {
            Debug.Log($"SAVE_MANAGER: Save {gameObject.name} ({uniqueId})");
            gameData.entities.Add(uniqueId, enemyHealth.isDead);
        }
        else
        {
            Debug.Log($"SAVE_MANAGER: Update {gameObject.name} ({uniqueId})");
            gameData.entities[uniqueId] = enemyHealth.isDead;
        }
    }

    public virtual void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load {gameObject.name} ({uniqueId})");

        // HANDLE ENEMY DEATH
        if (!gameData.entities.ContainsKey(uniqueId) || gameData.entities[uniqueId])
        {
            if (enemyHealth.isDead)
                return;

            enemyHealth.currentHealth = 0;
            enemyHealth.isDead = true;

            gameObject.SetActive(false);
        }
        else //HANDLE ENEMY LIVE
        {
            // Reset health and effect status
            enemyHealth.ResetHealth();
            entityHandleEffect.ResetHandleEffect();

            // Reset position
            transform.position = originPosition;

            // Reset state machine
            stateMachine.ChangeState(idleState);

            gameObject.SetActive(true);
        }
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
