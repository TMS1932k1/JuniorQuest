using System.Collections;
using UnityEngine;

public class Entity_RangedCombat : Entity_Combat
{
    [Header("Ranged Attack")]
    [SerializeField] float cooldownRangedAttack = 1f;
    [SerializeField] ObjectPool<FlyDemon_RangedAttack> objectPool;

    public bool isRangdAttack;
    private Coroutine RangdAttackCoroutine;


    public override void PerformAttack()
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

    private void CreateRangedAttack(GameObject target)
    {
        FlyDemon_RangedAttack attack = objectPool.GetObject();
        damage = stat.GetDamageWithCrit(out bool isCrit);

        attack.SetDetails(transform.position, CalculateAngleZ(target.transform), damage);
        attack.SetMove();
    }

    private Collider2D GetTargetCollider()
    {
        currentAttackCircle = attackCircles[attackCircleIndex];
        return Physics2D.OverlapCircle(currentAttackCircle.transform.position, currentAttackCircle.radius, whatIsTarget);
    }

    private float CalculateAngleZ(Transform targetTrans)
    {
        Vector3 dir = targetTrans.position - transform.position;

        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
