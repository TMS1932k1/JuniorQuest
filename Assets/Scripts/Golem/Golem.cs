using UnityEngine;

public class Golem : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EParamenter_Enemy.isIdle.ToString(), stateMachine, this);
        moveState = new Enemy_GroundMoveState(EParamenter_Enemy.isMove.ToString(), stateMachine, this);
        attackState = new Enemy_AttackState(EParamenter_Enemy.isAttack.ToString(), stateMachine, this);
        detectedState = new Golem_DetectedState(EParamenter_Enemy.isDetected.ToString(), stateMachine, this);
        deathState = new Enemy_DeathState(EParamenter_Enemy.isDeath.ToString(), stateMachine, this);
        stunnedState = new Enemy_StunnedState(EParamenter_Enemy.isStunned.ToString(), stateMachine, this);
        freezedState = new Enemy_FreezedState(EParamenter_Enemy.isFreezed.ToString(), stateMachine, this);
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
