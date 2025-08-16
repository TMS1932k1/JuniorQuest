using UnityEngine;

public class Enemy : Entity
{
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


    // States
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_PlayerDetectedState playerDetectedState;

    public float GetAnimSpeedMutiplier()
    {
        return moveDetectedSpeed / moveSpeed;
    }

    protected override void HandleCollisions()
    {
        base.HandleCollisions();

        isAttack = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToAttack, whatIsPlayer);
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
