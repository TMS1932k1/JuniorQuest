using System;
using UnityEngine;

public class Skill_IcePrison : Skill_Base
{
    [SerializeField] LayerMask whatIsFreeze;


    public override void PerformSkill()
    {
        base.PerformSkill();

        playerVFX.ShowIcePrisonVFX();
        playerSFX.PlayIcePrison();

        PerformFreeze();
    }

    private void PerformFreeze()
    {
        Collider2D[] freezeTargets = Physics2D.OverlapBoxAll(
            transform.position,
            new Vector2(skillData.widthArena, skillData.heightArena), 0,
            whatIsFreeze);

        foreach (Collider2D target in freezeTargets)
        {
            ICanFreeze freezedTarget = target.GetComponent<ICanFreeze>();
            if (freezedTarget != null)
            {
                freezedTarget.BeFreezed(skillData.effectDuration);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(skillData.widthArena, skillData.heightArena));
    }
}
