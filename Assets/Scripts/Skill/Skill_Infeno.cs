using System.Collections;
using UnityEngine;

public class Skill_Infeno : Skill_Base
{
    [Header("Infeno Arena")]
    public LayerMask whatIsBurn;


    private Entity_Stat stat;
    private ObjectPool_SkillInfeno pool;
    private Coroutine CreateArenaCoroutine;


    protected override void Awake()
    {
        base.Awake();

        stat = GetComponentInParent<Entity_Stat>();
        pool = GetComponent<ObjectPool_SkillInfeno>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        if (CreateArenaCoroutine != null)
            StopCoroutine(CreateArenaCoroutine);

        CreateArenaCoroutine = StartCoroutine(CreateArenaCo());
    }

    private IEnumerator CreateArenaCo()
    {
        // Show Infeno arena
        Skill_Infeno_Arena infenoArena = pool.GetObject();

        // Set arena details
        CalcullateDamage(out float finalDamage);
        infenoArena.SetArenaDetails(finalDamage, transform.position);

        // Hide then duration
        yield return new WaitForSeconds(skillData.duration);
        pool.ReturnObject(infenoArena);
    }


    protected override void CalcullateDamage(out float finalDamage)
    {
        base.CalcullateDamage(out finalDamage);

        // Damage burn = damage of skill + base damage no crit
        finalDamage = skillData.damage + skillData.effectPercent / 100 * stat.GetDamage();
    }
}
