using UnityEngine;

public class Bat : Enemy_Fly
{
    private Bat_SleepState sleepState;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EnemyAnimationStrings.idleAnim, stateMachine, this);
        moveState = new Enemy_FlyMoveState(EnemyAnimationStrings.moveAnim, stateMachine, this);
        detectedState = new Bat_DetectedState(EnemyAnimationStrings.detectedAnim, stateMachine, this);
        attackState = new Enemy_AttackState(EnemyAnimationStrings.attackAnim, stateMachine, this);
        deathState = new Enemy_DeathState(EnemyAnimationStrings.deathAnim, stateMachine, this);
        freezedState = new Enemy_FreezedState(EnemyAnimationStrings.freezedAnim, stateMachine, this);
        stunnedState = new Enemy_StunnedState(EnemyAnimationStrings.stunnedAnim, stateMachine, this);
        sleepState = new Bat_SleepState(BatAnimationStrings.sleepAnim, stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(sleepState);
    }

    protected override void HandleCollisions()
    {
        isAttack = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToAttack, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Line detect player to attack
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(distanceToAttack * faceDir, 0, 0));
    }
}
