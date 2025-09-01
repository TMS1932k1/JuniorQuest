using UnityEngine;

public class Skill_Infeno : Skill_Base
{
    [Header("Infeno Arena")]
    [SerializeField] Skill_Infeno_Arena infenoArena;
    [SerializeField] LayerMask whatIsBurn;

    private Entity_Stat stat;

    protected override void Awake()
    {
        base.Awake();

        stat = GetComponentInParent<Entity_Stat>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        // Show Infeno arena
        infenoArena.gameObject.SetActive(true);

        // Set arena details
        CalcullateDamage(out float finalDamage);
        infenoArena.SetArenaDetails(
            skillData,
            finalDamage,
            transform.position,
            whatIsBurn);

        // Hide arena after duration
        Invoke(nameof(HideInfenoArena), skillData.duration);
    }

    private void HideInfenoArena()
    {
        infenoArena.gameObject.SetActive(false);
    }

    protected override void CalcullateDamage(out float finalDamage)
    {
        base.CalcullateDamage(out finalDamage);

        // Damage burn = damage of skill + base damage no crit
        finalDamage = skillData.damage + skillData.effectPercent / 100 * stat.GetBaseDamageNoCrit();
    }
}
