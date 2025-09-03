using UnityEngine;

public class Enemy_Golem : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(Paramenter_Enemy.isIdle.ToString(), stateMachine, this);
        moveState = new Enemy_MoveState(Paramenter_Enemy.isMove.ToString(), stateMachine, this);
        attackState = new Enemy_AttackState(Paramenter_Enemy.isAttack.ToString(), stateMachine, this);
        playerDetectedState = new Enemy_PlayerDetectedState(Paramenter_Enemy.isPlayerDetected.ToString(), stateMachine, this);
        deathState = new Enemy_DeathState(Paramenter_Enemy.isDeath.ToString(), stateMachine, this);
        stunnedState = new Enemy_StunnedState(Paramenter_Enemy.isStunned.ToString(), stateMachine, this);
        freezedState = new Enemy_FreezedState(Paramenter_Enemy.isFreezed.ToString(), stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
