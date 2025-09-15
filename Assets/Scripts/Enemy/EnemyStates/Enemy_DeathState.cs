using UnityEngine;

public class Enemy_DeathState : EnemyState
{
    private Enemy_VFX enemyVFX;
    private float timeDestroy = 1f;


    public Enemy_DeathState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        enemyVFX = enemy.GetComponent<Enemy_VFX>();
    }

    public override void Enter()
    {
        base.Enter();

        ResetVFX();
        Object.Destroy(enemy, timeDestroy);
    }

    private void ResetVFX()
    {
        enemy.canStunned = false;
        enemyVFX.ResetVFX();
    }
}
