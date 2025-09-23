using UnityEngine;

public class Enemy_DeathState : EnemyState
{
    public Enemy_DeathState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isTrigger = false;

        enemyVFX.ResetVFX();
        entitySFX?.PlayDeath();

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
