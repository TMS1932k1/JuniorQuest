using System.Collections;
using UnityEngine;

public class Skill_BattleCry : Skill_Base
{
    private Coroutine IncrementStatCoroutine;


    public override void PerformSkill()
    {
        base.PerformSkill();

        playerVFX.ShowBattleCryVFX(skillData.duration);
        playerSFX.PlayBattleCry();

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
