using System;
using UnityEngine;

public class Skill_IcePrison : Skill_Base
{
    [SerializeField] LayerMask whatIsFreeze;


    public override void PerformSkill()
    {
        base.PerformSkill();

        playerVFX.ShowIcePrisonVFX();
        Freeze();
    }

    private void Freeze()
    {
        Collider2D[] freezeTargets = Physics2D.OverlapBoxAll(
            transform.position,
            new Vector2(skillData.widthArena, skillData.heightArena),
            whatIsFreeze);

        foreach (Collider2D target in freezeTargets)
        {
            ICanFreeze canFreezed = target.GetComponent<ICanFreeze>();
            if (canFreezed != null)
            {
                canFreezed.BeFreezed(skillData.effectDuration);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(skillData.widthArena, skillData.heightArena));
    }
}
