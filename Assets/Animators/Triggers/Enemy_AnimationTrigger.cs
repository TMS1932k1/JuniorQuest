using UnityEngine;

public class Enemy_AnimationTrigger : Entity_AnimationTrigger
{
    private Enemy enemy;
    private Enemy_VFX enemyVFX;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
        enemyVFX = GetComponentInParent<Enemy_VFX>();
    }

    private void EnableCounter()
    {
        enemy.canStunned = true;
        enemyVFX.EnableCounterAlert(true);
    }

    private void DisableCounter()
    {
        enemy.canStunned = false;
        enemyVFX.EnableCounterAlert(false);
    }
}
