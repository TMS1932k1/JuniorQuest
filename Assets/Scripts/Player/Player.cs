using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
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


    public bool isDead;
    private Player_XP playerXP;


    void OnEnable()
    {
        Enemy.OnEnemyDeath += HandleXPReceive;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDeath -= HandleXPReceive;
    }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Player_IdleState(Paramenter_Player.isIdle.ToString(), stateMachine, this);
        moveState = new Player_MoveState(Paramenter_Player.isMove.ToString(), stateMachine, this);
        jumpState = new Player_JumpState(Paramenter_Player.isJumpFall.ToString(), stateMachine, this);
        fallState = new Player_FallState(Paramenter_Player.isJumpFall.ToString(), stateMachine, this);
        wallSlideState = new Player_WallSlideState(Paramenter_Player.isWallSlide.ToString(), stateMachine, this);
        wallJumpState = new Player_WallJumpState(Paramenter_Player.isWallJump.ToString(), stateMachine, this);
        dashState = new Player_DashState(Paramenter_Player.isDash.ToString(), stateMachine, this);
        basicAttackState = new Player_BasicAttackState(Paramenter_Player.isAttack.ToString(), stateMachine, this);
        slideState = new Player_SlideState(Paramenter_Player.isSlide.ToString(), stateMachine, this);
        hurtState = new Player_HurtState(Paramenter_Player.isHurt.ToString(), stateMachine, this);
        deathState = new Player_DeathState(Paramenter_Player.isDeath.ToString(), stateMachine, this);
        counterState = new Player_CounterState(Paramenter_Player.isCounter.ToString(), stateMachine, this);
        fireBladeState = new Player_FireBladeState(Paramenter_Player.isFireBlade.ToString(), stateMachine, this);

        playerXP = GetComponent<Player_XP>();
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
}
