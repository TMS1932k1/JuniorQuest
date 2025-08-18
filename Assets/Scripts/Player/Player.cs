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
    public float slideDuration;
    public float slideSpeed;
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

    public bool isDead;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Player_IdleState("isIdle", stateMachine, this);
        moveState = new Player_MoveState("isMove", stateMachine, this);
        jumpState = new Player_JumpState("isJumpFall", stateMachine, this);
        fallState = new Player_FallState("isJumpFall", stateMachine, this);
        wallSlideState = new Player_WallSlideState("isWallSlide", stateMachine, this);
        wallJumpState = new Player_WallJumpState("isWallJump", stateMachine, this);
        dashState = new Player_DashState("isDash", stateMachine, this);
        basicAttackState = new Player_BasicAttackState("isAttack", stateMachine, this);
        slideState = new Player_SlideState("isSlide", stateMachine, this);
        hurtState = new Player_HurtState("isHurt", stateMachine, this);
        deathState = new Player_DeathState("isDeath", stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
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
