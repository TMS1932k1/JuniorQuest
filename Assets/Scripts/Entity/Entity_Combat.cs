using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    protected Entity_VFX entityVFX;
    private Entity_Stat stat;

    [Header("Attack Circles Check")]
    [SerializeField] private AttackCircle[] attackCircles;
    [SerializeField] private LayerMask whatIsTarget;

    private int attackCircleIndex;
    private float damage;
    private AttackCircle currentAttackCircle;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        stat = GetComponent<Entity_Stat>();
    }

    public void PerformAttack()
    {
        foreach (Collider2D target in GetTargetColliders())
        {
            PerformDamage(target);
        }
    }

    private void PerformDamage(Collider2D target)
    {
        bool isCrit = IsCrit();
        damage = CalculateDamage(isCrit);

        target.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, transform);

        if (!isMissed)
            entityVFX.CreateHitVFX(target.transform.position, isCrit);

    }

    /// <summary>
    /// Calculate damage with crit details
    /// </summary>
    /// <returns>Finish damage</returns>
    private float CalculateDamage(bool isCrit)
    {
        if (isCrit)
        {
            return stat.GetDamage() * (1 + stat.GetCritPower() / 100);
        }
        else
        {
            return stat.GetDamage();
        }
    }

    private bool IsCrit()
    {
        return Random.Range(0, 100) <= stat.GetCritChange();
    }

    /// <summary>
    /// Get all targets which are (whatIsTarget), in (attackCircles)
    /// </summary>
    /// <returns>Return Collider2D[] to perform attack</returns>
    private Collider2D[] GetTargetColliders()
    {
        if (attackCircleIndex > attackCircles.Length - 1) attackCircleIndex = 0;

        currentAttackCircle = attackCircles[attackCircleIndex];
        return Physics2D.OverlapCircleAll(currentAttackCircle.transform.position, currentAttackCircle.radius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        foreach (AttackCircle attackCircle in attackCircles)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackCircle.transform.position, attackCircle.radius);
        }
    }

    public void SetAttackCircleIndex(int index)
    {
        attackCircleIndex = index;
    }
}
