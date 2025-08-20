using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;
    public Transform damageTransform { get; private set; }

    private Entity_VFX entityVFX;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
        entityVFX = GetComponent<Entity_VFX>();
    }

    public override void ReduceHealth(float damage, Transform damageDealer)
    {
        base.ReduceHealth(damage, damageDealer);

        if (!isDead)
        {
            // Damage VFX
            entityVFX.PlayOnDamageVFXCO();

            // Detect Player
            ChangePlayerDectectedState(damageDealer);
        }
    }

    protected override void Die()
    {
        base.Die();
        enemy.OnDead();
    }

    /// <summary>
    /// Change to (PlayerDetectedState) when player damage behind enemy
    /// </summary>
    /// <param name="damageTransform">Transform of player to reference in (PlayerDetectedState)</param>
    private void ChangePlayerDectectedState(Transform damageTransform)
    {
        if (enemy.GetCurrentState() != enemy.attackState && enemy.GetCurrentState() != enemy.playerDetectedState)
        {
            this.damageTransform = damageTransform;
            enemy.stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
}


