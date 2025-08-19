using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    private Enemy_VFX enemyVFX;

    public Enemy_StunnedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        enemyVFX = enemy.GetComponent<Enemy_VFX>();
    }

    public override void Enter()
    {
        base.Enter();

        OffCanCounter();

        stateTimer = enemy.stunnedDuration;
        rb.linearVelocity = new Vector2(enemy.stunnedVelocity.x * -enemy.faceDir, enemy.stunnedVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    private void OffCanCounter()
    {
        enemy.canStunned = false;
        enemyVFX.EnableCounterAlert(false);
    }
}
