using UnityEngine;

public class FlyDemon : Enemy_Fly
{

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EParamenter_Enemy.isIdle.ToString(), stateMachine, this);
        moveState = new Enemy_FlyMoveState(EParamenter_Enemy.isMove.ToString(), stateMachine, this);
        detectedState = new FlyDemon_DetectedState(EParamenter_Enemy.isDetected.ToString(), stateMachine, this);
        attackState = new Enemy_AttackState(EParamenter_Enemy.isAttack.ToString(), stateMachine, this);
        deathState = new Enemy_DeathState(EParamenter_Enemy.isDeath.ToString(), stateMachine, this);
        freezedState = new Enemy_FreezedState(EParamenter_Enemy.isFreezed.ToString(), stateMachine, this);
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
