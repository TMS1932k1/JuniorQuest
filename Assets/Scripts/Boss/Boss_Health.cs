using UnityEngine;

public class Boss_Health : Entity_Health
{
    private Boss boss;
    private Entity_VFX entityVFX;


    protected override void Awake()
    {
        base.Awake();

        boss = GetComponent<Boss>();
        entityVFX = GetComponent<Entity_VFX>();
    }

    /// <summary>
    /// Reduce Health by player
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="isMissed"></param>
    /// <param name="damageDealer">Transform of player</param>
    public override void ReduceHealth(float damage, out bool isMissed, Transform damageDealer)
    {
        base.ReduceHealth(damage, out isMissed, damageDealer);

        if (!isDead)
        {
            // Damage VFX
            if (!isMissed)
                entityVFX.PlayOnDamageVFXCo();
        }
    }

    public override void Die()
    {
        base.Die();
        boss.OnDead();
    }
}
