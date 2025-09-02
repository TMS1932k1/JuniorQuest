using System.Collections;
using UnityEngine;

public class Skill_ShieldBarrier : Skill_Base
{
    private Entity_Stat stat;
    private Coroutine CreateShieldCoroutine;


    protected override void Awake()
    {
        base.Awake();

        stat = GetComponentInParent<Entity_Stat>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        playerVFX.ShowShieldBarrierVFX(skillData.duration); // VFX
        ReduceTakeDamge();
    }

    private void ReduceTakeDamge()
    {
        if (CreateShieldCoroutine != null)
            StopCoroutine(CreateShieldCoroutine);

        CreateShieldCoroutine = StartCoroutine(IncrementStatCo());
    }

    private IEnumerator IncrementStatCo()
    {
        stat.AddModifierWithType(StatType.Mitigation, skillData.skillName, skillData.effectPercent);
        yield return new WaitForSeconds(skillData.duration);
        stat.RemoveModifierWithType(StatType.Mitigation, skillData.skillName);
    }
}
