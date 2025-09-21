using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected string uniqueId;


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


    [Header("Knockback")]
    [SerializeField] private float normalKnockBackDuration;
    [SerializeField] private Vector2 normalKnockBackPosition;
    [SerializeField] private float heavyKnockBackDuration;
    [SerializeField] private Vector2 heavyKnockBackPosition;
    private Coroutine knockBackCoroutine;
    public bool isKnocked;


    // State machine
    public StateMachine stateMachine { get; private set; }


    // Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    private Entity_HandleEffect entityHandleEffect;


    public int faceDir { get; protected set; } = 1;


    protected virtual void Awake()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entityHandleEffect = GetComponent<Entity_HandleEffect>();

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
        if (isKnocked)
            return;

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

    public void ReceiveKnockBack(bool isHeavyAttack, int knockBackDir)
    {
        if (knockBackCoroutine != null)
            StopCoroutine(knockBackCoroutine);

        Vector2 knockBackPosition = (isHeavyAttack ? heavyKnockBackPosition : normalKnockBackPosition) * new Vector2(knockBackDir, 1);
        float knockBackDuration = isHeavyAttack ? heavyKnockBackDuration : normalKnockBackDuration;

        knockBackCoroutine = StartCoroutine(KnockBackCO(knockBackPosition, knockBackDuration));
    }

    private IEnumerator KnockBackCO(Vector2 knockBackPosition, float knockBackDuration)
    {
        isKnocked = true;
        rb.linearVelocity = knockBackPosition;

        yield return new WaitForSeconds(knockBackDuration);

        isKnocked = false;
        rb.linearVelocity = new Vector2(0, 0);
    }

    public virtual void OnDead()
    {
        entityHandleEffect.ResetHandleEffect();
    }

    public void CallTriggerCurrentState()
    {
        stateMachine.currentState.CallTrigger();
    }

    public EntityState GetCurrentState()
    {
        return stateMachine.currentState;
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

    public abstract void BeFreezed(float duration);
    public abstract void ExitFreezed();

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}
