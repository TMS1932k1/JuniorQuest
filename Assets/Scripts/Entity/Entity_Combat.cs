using System.Linq;
using UnityEngine;

[System.Serializable]
public class AttackCircle
{
    public Transform transform;
    public float radius;
}

public class Entity_Combat : MonoBehaviour, ICombat
{
    protected Entity_VFX entityVFX;
    protected Entity_Stat stat;


    [Header("Attack Circles Check")]
    [SerializeField] protected AttackCircle[] attackCircles;
    [SerializeField] protected LayerMask whatIsTarget;


    protected int attackCircleIndex;
    protected float damage;
    protected AttackCircle currentAttackCircle;


    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        stat = GetComponent<Entity_Stat>();
    }

    public virtual void PerformAttack()
    {
        foreach (Collider2D target in GetTargetColliders())
        {
            // If break obj then destroy it
            if (target.GetComponent<IBreakable>() != null)
            {
                Break(target.GetComponent<IBreakable>());
                continue;
            }

            // Other damage
            PerformDamage(target);
        }
    }

    private void Break(IBreakable target)
    {
        target.Break();
    }

    private void PerformDamage(Collider2D target)
    {
        damage = stat.GetDamageWithCrit(out bool isCrit);

        Entity_Health targetHealth = target.gameObject.GetComponent<Entity_Health>();

        if (!targetHealth.isDead)
        {
            targetHealth.ReduceHealth(damage, out bool isMissed, transform);

            if (!isMissed)
                entityVFX.CreateHitVFX(target.transform.position, isCrit);
        }
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

    public void SetAttackCircleIndex(int index)
    {
        attackCircleIndex = index;
    }

    private void OnDrawGizmos()
    {
        foreach (AttackCircle attackCircle in attackCircles)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackCircle.transform.position, attackCircle.radius);
        }
    }
}
