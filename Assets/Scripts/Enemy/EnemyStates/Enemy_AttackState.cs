using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    private Enemy_VFX enemyVFX;


    public Enemy_AttackState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        enemyVFX = enemy.GetComponent<Enemy_VFX>();
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
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (enemy.canStunned)
        {
            enemyVFX.EnableCounterAlert(false);
            enemy.canStunned = false;
        }
    }

    public override void CallTrigger()
    {
        base.CallTrigger();
    }
}
