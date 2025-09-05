using UnityEngine;

public class Skill_FireBlade : Skill_Base
{
    private ObjectPool_FireBlade pool;

    private Entity_Stat stat;

    private Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        stat = GetComponentInParent<Entity_Stat>();
        pool = GetComponent<ObjectPool_FireBlade>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        player.stateMachine.ChangeState(player.fireBladeState);
    }

    public void CreateFireBlade(float angleZ)
    {
        Skill_FireBlade_Slash slash = pool.GetObject();

        CalcullateDamage(out float finalDamage);
        slash.SetBladeDetails(finalDamage, skillData.effectDuration);
        slash.transform.rotation = Quaternion.Euler(0, 0, angleZ);

        slash.SetMove();

    }

    protected override void CalcullateDamage(out float finalDamage)
    {
        base.CalcullateDamage(out finalDamage);

        // Slash damage = 200% base damage
        finalDamage = stat.GetDamageWithCrit(out bool isCrit) * skillData.effectPercent / 100;
    }
}
