using System.Collections;
using UnityEngine;

public class Skill_BattleCry : Skill_Base
{
    private Entity_Stat stat;
    private Coroutine IncrementStatCoroutine;


    protected override void Awake()
    {
        base.Awake();

        stat = GetComponentInParent<Entity_Stat>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        playerVFX.ShowBattleCryVFX(skillData.duration); // VFX
        IncrementStat();
    }

    private void IncrementStat()
    {
        if (IncrementStatCoroutine != null)
            StopCoroutine(IncrementStatCoroutine);

        IncrementStatCoroutine = StartCoroutine(IncrementStatCo());
    }

    private IEnumerator IncrementStatCo()
    {
        stat.AddAllModifierWithPercent(skillData.skillName, skillData.effectPercent / 100f);
        yield return new WaitForSeconds(skillData.duration);
        stat.RemoveAllModifierWithPercent(skillData.skillName);
    }
}
