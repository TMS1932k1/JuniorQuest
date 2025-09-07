using UnityEngine;

public class Bat : Enemy_Fly
{
    private Bat_SleepState sleepState;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(EParamenter_Enemy.isIdle.ToString(), stateMachine, this);
        moveState = new Enemy_FlyMoveState(EParamenter_Enemy.isMove.ToString(), stateMachine, this);
        detectedState = new Bat_DetectedState(EParamenter_Enemy.isDetected.ToString(), stateMachine, this);
        attackState = new Enemy_AttackState(EParamenter_Enemy.isAttack.ToString(), stateMachine, this);
        deathState = new Enemy_DeathState(EParamenter_Enemy.isDeath.ToString(), stateMachine, this);
        freezedState = new Enemy_FreezedState(EParamenter_Enemy.isFreezed.ToString(), stateMachine, this);
        stunnedState = new Enemy_StunnedState(EParamenter_Enemy.isStunned.ToString(), stateMachine, this);
        sleepState = new Bat_SleepState(EParamenter_Enemy.isSleep.ToString(), stateMachine, this);
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
