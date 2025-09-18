using UnityEngine;

public class FlyDemon : Enemy_Fly
{

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EnemyAnimationStrings.idleAnim, stateMachine, this);
        moveState = new Enemy_FlyMoveState(EnemyAnimationStrings.moveAnim, stateMachine, this);
        detectedState = new FlyDemon_DetectedState(EnemyAnimationStrings.detectedAnim, stateMachine, this);
        attackState = new Enemy_AttackState(EnemyAnimationStrings.attackAnim, stateMachine, this);
        deathState = new Enemy_DeathState(EnemyAnimationStrings.deathAnim, stateMachine, this);
        freezedState = new Enemy_FreezedState(EnemyAnimationStrings.stunnedAnim, stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void HandleCollisions()
    {
        isAttack = Physics2D.OverlapCircle(transform.position, distanceToDetectPlayer, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Line detect player to attack
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToAttack);
    }
}
