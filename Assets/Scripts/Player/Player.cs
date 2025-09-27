using System;
using System.Collections;
using UnityEngine;

public class Player : Entity, ISaveable
{
    public static event Action OnPlayerDeath;


    [Header("Move details")]
    public float moveSpeed;
    public float jumpForce;
    public float dashDuration;
    public float dashSpeed;
    public float dashCooldown;
    public float slideDuration;
    public float slideSpeed;
    public float slideCooldown;
    [Range(0, 1)]
    public float airMoveMultiplier;
    [Range(0, 1)]
    public float wallSlideMultiplier;
    public Vector2 wallJumpVelocity;


    [Header("Attack details")]
    public Vector2[] attackVelocities;
    private Coroutine AttackQueueCo;


    // States
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_SlideState slideState { get; private set; }
    public Player_HurtState hurtState { get; private set; }
    public Player_DeathState deathState { get; private set; }
    public Player_CounterState counterState { get; private set; }
    public Player_FireBladeState fireBladeState { get; private set; }


    private Player_XP playerXP;

    public bool isDead;


    protected override void Awake()
    {
        base.Awake();

        idleState = new Player_IdleState(PlayerAnimationStrings.IDLE_ANIM, stateMachine, this);
        moveState = new Player_MoveState(PlayerAnimationStrings.MOVE_ANIM, stateMachine, this);
        jumpState = new Player_JumpState(PlayerAnimationStrings.JUMP_FALL_ANIM, stateMachine, this);
        fallState = new Player_FallState(PlayerAnimationStrings.JUMP_FALL_ANIM, stateMachine, this);
        wallSlideState = new Player_WallSlideState(PlayerAnimationStrings.WALL_SLIDE_ANIM, stateMachine, this);
        wallJumpState = new Player_WallJumpState(PlayerAnimationStrings.WALL_JUMP_ANIM, stateMachine, this);
        dashState = new Player_DashState(PlayerAnimationStrings.DASH_ANIM, stateMachine, this);
        basicAttackState = new Player_BasicAttackState(PlayerAnimationStrings.ATTACK_ANIM, stateMachine, this);
        slideState = new Player_SlideState(PlayerAnimationStrings.SLIDE_ANIM, stateMachine, this);
        hurtState = new Player_HurtState(PlayerAnimationStrings.HURT_ANIM, stateMachine, this);
        deathState = new Player_DeathState(PlayerAnimationStrings.DEATH_ANIM, stateMachine, this);
        counterState = new Player_CounterState(PlayerAnimationStrings.COUNTER_ANIM, stateMachine, this);
        fireBladeState = new Player_FireBladeState(PlayerAnimationStrings.FIRE_BLADE_ANIM, stateMachine, this);

        playerXP = GetComponent<Player_XP>();
    }

    void OnEnable()
    {
        Enemy.OnEnemyDeath += HandleXPReceive;
        Boss.OnBossDeath += HandleXPReceive;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDeath -= HandleXPReceive;
        Boss.OnBossDeath -= HandleXPReceive;
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    private void HandleXPReceive(float xp)
    {
        playerXP.IncrementXP(xp);
    }

    public override void OnDead()
    {
        base.OnDead();

        OnPlayerDeath?.Invoke();
        stateMachine.ChangeState(deathState);
    }

    /// <summary>
    /// When clicking continuously to attack but not exit current state 
    /// thenyou need to queue to perform later.
    /// </summary>
    /// <param name="state">State need change</param>
    public void EnterAttackQueue(PlayerState state)
    {
        if (AttackQueueCo != null)
        {
            StopCoroutine(AttackQueueCo);
        }

        AttackQueueCo = StartCoroutine(EnterAttackQueueCo(state));
    }

    private IEnumerator EnterAttackQueueCo(PlayerState state)
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(state);
    }

    public override void BeFreezed(float duration)
    {
        // Code change freezed state
    }

    public override void ExitFreezed()
    {
        // Code out freezed state
    }

    public void SaveData(ref GameData gameData)
    {

    }

    public void LoadData(GameData gameData)
    {
        if (gameData.position != null)
            transform.position = gameData.position;
        else
            SaveManager.instance.SavePosition(transform.position);

        stateMachine.ChangeState(idleState);
    }
}
