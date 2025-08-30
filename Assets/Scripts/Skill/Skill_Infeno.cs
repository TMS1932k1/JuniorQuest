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
        infenoArena.SetArenaDetails(
            skillData,
            CalcullateDamageBurn(),
            transform.position,
            whatIsBurn);

        // Hide arena after duration
        Invoke(nameof(HideInfenoArena), skillData.duration);
    }

    private void HideInfenoArena()
    {
        infenoArena.gameObject.SetActive(false);
    }

    private float CalcullateDamageBurn()
    {
        // Damage burn = damage of skill + base damage no crit
        return skillData.damage + skillData.effectPercent / 100 * stat.GetBaseDamageNoCrit();
    }
}
