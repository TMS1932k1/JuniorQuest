using UnityEngine;

public class FlyDemon : Enemy_Fly
{

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(Paramenter_Enemy.isIdle.ToString(), stateMachine, this);
        moveState = new Enemy_FlyMoveState(Paramenter_Enemy.isMove.ToString(), stateMachine, this);
        detectedState = new FlyDemon_DetectedState(Paramenter_Enemy.isDetected.ToString(), stateMachine, this);
        attackState = new Enemy_AttackState(Paramenter_Enemy.isAttack.ToString(), stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    /// <summary>
    /// Detect player with (distanceToDetectPlayer)
    /// if not detect then return (null)
    /// </summary>
    /// <returns></returns>
    public override Collider2D DetectPlayer()
    {
        playerDetect = Physics2D.OverlapCircle(transform.position, distanceToDetectPlayer, whatIsPlayer);
        return playerDetect;
    }

    protected override void HandleCollisions()
    {
        isAttack = Physics2D.OverlapCircle(transform.position, distanceToDetectPlayer, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        // Line detect player to attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToDetectPlayer);

        // Line detect player to attack
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToAttack);
    }
}
