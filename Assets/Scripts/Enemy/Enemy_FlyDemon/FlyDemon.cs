using UnityEngine;

public class FlyDemon : Enemy_Fly
{

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EnemyAnimationStrings.IDLE_ANIM, stateMachine, this);
        moveState = new Enemy_FlyMoveState(EnemyAnimationStrings.MOVE_ANIM, stateMachine, this);
        detectedState = new FlyDemon_DetectedState(EnemyAnimationStrings.DETECTED_ANIM, stateMachine, this);
        attackState = new Enemy_AttackState(EnemyAnimationStrings.ATTACK_ANIM, stateMachine, this);
        deathState = new Enemy_DeathState(EnemyAnimationStrings.DEATH_ANIM, stateMachine, this);
        freezedState = new Enemy_FreezedState(EnemyAnimationStrings.STUNNED_ANIM, stateMachine, this);
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
