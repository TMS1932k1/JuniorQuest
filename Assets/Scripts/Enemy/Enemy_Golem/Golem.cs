using UnityEngine;

public class Golem : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EnemyAnimationStrings.idleAnim, stateMachine, this);
        moveState = new Enemy_GroundMoveState(EnemyAnimationStrings.moveAnim, stateMachine, this);
        attackState = new Enemy_AttackState(EnemyAnimationStrings.attackAnim, stateMachine, this);
        detectedState = new Golem_DetectedState(EnemyAnimationStrings.detectedAnim, stateMachine, this);
        deathState = new Enemy_DeathState(EnemyAnimationStrings.deathAnim, stateMachine, this);
        stunnedState = new Enemy_StunnedState(EnemyAnimationStrings.stunnedAnim, stateMachine, this);
        freezedState = new Enemy_FreezedState(EnemyAnimationStrings.freezedAnim, stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void HandleCollisions()
    {
        base.HandleCollisions();

        isAttack = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToAttack, whatIsPlayer);
    }
}
