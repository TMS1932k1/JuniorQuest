using UnityEngine;

public class Skill_FireBlade : Skill_Base
{
    [SerializeField] Skill_FireBlade_Slash slash;

    private Entity_Stat stat;

    private Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        stat = GetComponentInParent<Entity_Stat>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        player.stateMachine.ChangeState(player.fireBladeState);
    }

    public void CreateFireBlade(float angleZ)
    {
        slash.gameObject.SetActive(true);

        // Set Damage
        CalcullateDamage(out float finalDamage);
        slash.SetBladeDetails(finalDamage, skillData.effectDuration);

        // Set Rotate
        slash.transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }

    protected override void CalcullateDamage(out float finalDamage)
    {
        base.CalcullateDamage(out finalDamage);

        // Slash damage = 200% base damage
        finalDamage = stat.GetDamage(out bool isCrit) * skillData.effectPercent / 100;
    }
}
