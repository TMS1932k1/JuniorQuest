using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move details")]
    public float moveSpeed;
    public float jumpForce;
    public float dashDuration;
    public float dashSpeed;
    [Range(0, 1)]
    public float airMoveMultiplier;
    [Range(0, 1)]
    public float wallSlideMultiplier;
    public Vector2 wallJumpVelocity;


    [Header("Attack details")]
    public Vector2[] attackVelocities;
    private Coroutine AttackQueueCo;


    [Header("Ground detection")]
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask whatIsGround;
    public bool groundDetect { get; private set; }


    [Header("Wall detection")]
    [SerializeField] float wallCheckDistance;
    [SerializeField] float primaryWallCheckVelocityY;
    [SerializeField] float secondaryWallCheckVelocityY;
    [SerializeField] LayerMask whatIsWall;
    public bool wallDetect { get; private set; }
    private Vector3 primaryWallCheckPosition;
    private Vector3 secondaryWallCheckPosition;

    // States
    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }

    // Compoments
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    public int faceDir { get; private set; } = 1;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        idleState = new Player_IdleState("isIdle", stateMachine, this);
        moveState = new Player_MoveState("isMove", stateMachine, this);
        jumpState = new Player_JumpState("isJumpFall", stateMachine, this);
        fallState = new Player_FallState("isJumpFall", stateMachine, this);
        wallSlideState = new Player_WallSlideState("isWallSlide", stateMachine, this);
        wallJumpState = new Player_WallJumpState("isWallJump", stateMachine, this);
        dashState = new Player_DashState("isDash", stateMachine, this);
        basicAttackState = new Player_BasicAttackState("isAttack", stateMachine, this);
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.CurrentStateUpdate();
        HandleCollisions();
    }

    private void HandleCollisions()
    {
        groundDetect = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        primaryWallCheckPosition = transform.position + new Vector3(0, primaryWallCheckVelocityY, 0);
        secondaryWallCheckPosition = transform.position + new Vector3(0, secondaryWallCheckVelocityY, 0);
        wallDetect = Physics2D.Raycast(primaryWallCheckPosition, Vector2.right * faceDir, wallCheckDistance, whatIsWall) &&
                    Physics2D.Raycast(secondaryWallCheckPosition, Vector2.right * faceDir, wallCheckDistance, whatIsWall);
    }

    public void SetVelocity(float x, float y)
    {
        rb.linearVelocity = new Vector2(x, y);

        // Hanlde Flip if backmovement
        if (faceDir == 1 && rb.linearVelocityX < 0)
        {
            Flip();
        }
        else if (faceDir == -1 && rb.linearVelocityX > 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        faceDir *= -1;
    }

    public void CallTriggerCurrentState()
    {
        stateMachine.currentState.CallTrigger();
    }

    /// <summary>
    /// When clicking continuously to attack but not exit current state 
    /// thenyou need to queue to perform later.
    /// </summary>
    /// <param name="state">State need change</param>
    public void EnterAttackQueue(EntityState state)
    {
        if (AttackQueueCo != null)
        {
            StopCoroutine(AttackQueueCo);
        }

        AttackQueueCo = StartCoroutine(EnterAttackQueueCo(state));
    }

    private IEnumerator EnterAttackQueueCo(EntityState state)
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(state);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));

        Vector3 WallCheck1 = transform.position + new Vector3(0, primaryWallCheckVelocityY, 0);
        Gizmos.DrawLine(WallCheck1, WallCheck1 + new Vector3(wallCheckDistance * faceDir, 0, 0));

        Vector3 WallCheck2 = transform.position + new Vector3(0, secondaryWallCheckVelocityY, 0);
        Gizmos.DrawLine(WallCheck2, WallCheck2 + new Vector3(wallCheckDistance * faceDir, 0, 0));
    }
}
