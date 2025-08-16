using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Ground detection")]
    [SerializeField] protected Transform groundCheckTransform;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public bool groundDetect { get; private set; }


    [Header("Wall detection")]
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float primaryWallCheckVelocityY;
    [SerializeField] protected float secondaryWallCheckVelocityY;
    [SerializeField] protected LayerMask whatIsWall;
    public bool wallDetect { get; private set; }
    protected Vector3 primaryWallCheckPosition;
    protected Vector3 secondaryWallCheckPosition;


    // State machine
    public StateMachine stateMachine { get; private set; }


    // Compoments
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    public int faceDir { get; private set; } = 1;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.CurrentStateUpdate();
        HandleCollisions();
    }

    protected virtual void HandleCollisions()
    {
        groundDetect = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, groundCheckDistance, whatIsGround);

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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Line ground detect
        Gizmos.DrawLine(groundCheckTransform.position, groundCheckTransform.position + new Vector3(0, -groundCheckDistance, 0));

        // Line wall detect 1
        Vector3 WallCheck1 = transform.position + new Vector3(0, primaryWallCheckVelocityY, 0);
        Gizmos.DrawLine(WallCheck1, WallCheck1 + new Vector3(wallCheckDistance * faceDir, 0, 0));

        // Line wall detect 2
        Vector3 WallCheck2 = transform.position + new Vector3(0, secondaryWallCheckVelocityY, 0);
        Gizmos.DrawLine(WallCheck2, WallCheck2 + new Vector3(wallCheckDistance * faceDir, 0, 0));
    }
}
