using UnityEngine;

public class Golem : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EnemyAnimationStrings.IDLE_ANIM, stateMachine, this);
        moveState = new Enemy_GroundMoveState(EnemyAnimationStrings.MOVE_ANIM, stateMachine, this);
        attackState = new Enemy_AttackState(EnemyAnimationStrings.ATTACK_ANIM, stateMachine, this);
        detectedState = new Golem_DetectedState(EnemyAnimationStrings.DETECTED_ANIM, stateMachine, this);
        deathState = new Enemy_DeathState(EnemyAnimationStrings.DEATH_ANIM, stateMachine, this);
        stunnedState = new Enemy_StunnedState(EnemyAnimationStrings.STUNNED_ANIM, stateMachine, this);
        freezedState = new Enemy_FreezedState(EnemyAnimationStrings.FREEZED_ANIM, stateMachine, this);
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
