using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {

    }

    public override void Enter()
    {
        base.Enter();

        isTrigger = false;
    }

    public override void Update()
    {
        base.Update();

        if (isTrigger)
        {
            stateMachine.ChangeState(enemy.detectedState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (enemy.canStunned)
        {
            enemyVFX?.EnableCounterAlert(false);
            enemy.canStunned = false;
        }
    }
}
