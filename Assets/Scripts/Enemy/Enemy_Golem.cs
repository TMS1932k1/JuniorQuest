using UnityEngine;

public class Enemy_Golem : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState("isIdle", stateMachine, this);
        moveState = new Enemy_MoveState("isMove", stateMachine, this);
        attackState = new Enemy_AttackState("isAttack", stateMachine, this);
        playerDetectedState = new Enemy_PlayerDetectedState("isPlayerDetected", stateMachine, this);
        deathState = new Enemy_DeathState("isDeath", stateMachine, this);
        stunnedState = new Enemy_StunnedState("isStunned", stateMachine, this);
        freezedState = new Enemy_FreezedState("isFreezed", stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
