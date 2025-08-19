using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    protected Entity_VFX entityVFX;

    [Header("Attack Circles Check")]
    [SerializeField] private AttackCircle[] attackCircles;
    [SerializeField] private LayerMask whatIsTarget;

    private int attackCircleIndex;
    private float damage;
    private AttackCircle currentAttackCircle;

    void Start()
    {
        entityVFX = GetComponent<Entity_VFX>();
    }

    public void PerformAttack()
    {
        foreach (Collider2D target in GetTargetColliders())
        {
            damage = attackCircles[attackCircleIndex].damage;

            target.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, transform);
            entityVFX.CreateHitVFX(target.transform.position);
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
