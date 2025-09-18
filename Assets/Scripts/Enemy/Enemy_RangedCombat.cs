using System.Collections;
using UnityEngine;

public abstract class Enemy_RangedCombat<T> : MonoBehaviour, ICombat where T : Component
{
    [Header("Ranged Attack")]
    [SerializeField] float cooldownRangedAttack = 1f;
    [SerializeField] LayerMask whatIsTarget;
    [SerializeField] float rangeRadius;
    [SerializeField] protected ObjectPool<T> objectPool;


    public bool isRangdAttack;
    private Coroutine RangdAttackCoroutine;
    protected float damage;


    protected Entity_Stat stat;


    private void Awake()
    {
        stat = GetComponent<Entity_Stat>();
    }

    public void PerformAttack()
    {
        GameObject target = GetTargetCollider()?.gameObject;

        if (target == null)
            return;

        if (RangdAttackCoroutine != null)
            StopCoroutine(RangdAttackCoroutine);

        RangdAttackCoroutine = StartCoroutine(RangedAttackCo(target));
    }

    /// <summary>
    /// Delay between ranged attacks
    /// </summary>
    /// <param name="target">To calculate angleZ</param>
    /// <returns></returns>
    private IEnumerator RangedAttackCo(GameObject target)
    {
        isRangdAttack = true;
        CreateRangedAttack(target);

        yield return new WaitForSeconds(cooldownRangedAttack);
        isRangdAttack = false;
    }

    protected abstract void CreateRangedAttack(GameObject target);

    private Collider2D GetTargetCollider()
    {
        return Physics2D.OverlapCircle(transform.position, rangeRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
