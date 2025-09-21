using UnityEngine;

public class Enemy_DeathState : EnemyState
{
    private Enemy_VFX enemyVFX;


    public Enemy_DeathState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        enemyVFX = enemy.GetComponent<Enemy_VFX>();
    }

    public override void Enter()
    {
        base.Enter();
        isTrigger = false;

        // Reset VFX
        enemyVFX.ResetVFX();
        enemy.canStunned = false;
    }

    public override void Update()
    {
        base.Update();

        if (isTrigger)
        {
            stateMachine.ChangeState(enemy.idleState);
            enemy.gameObject.SetActive(false);
        }
    }
}
