using UnityEngine;

public class FlyDemon_Combat : Enemy_RangedCombat<FlyDemon_RangedAttack>
{
    protected override void CreateRangedAttack(GameObject target)
    {
        FlyDemon_RangedAttack attackObj = objectPool.GetObject();
        damage = stat.GetDamageWithCrit(out bool isCrit);

        attackObj.SetDetails(transform.position, CalculateAngleZ(target.transform), damage);
        attackObj.SetMove();
    }

    private float CalculateAngleZ(Transform targetTrans)
    {
        Vector3 dir = targetTrans.position - transform.position;

        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
