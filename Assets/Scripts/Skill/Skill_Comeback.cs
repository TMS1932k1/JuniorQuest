using UnityEngine;

public class Skill_Comeback : Skill_Base
{
    private Player_VFX playerVFX;
    private Player_Health playerHealth;

    protected override void Awake()
    {
        base.Awake();

        playerVFX = GetComponentInParent<Player_VFX>();
        playerHealth = GetComponentInParent<Player_Health>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        SetLastTimeUsed(); // Cooldowntimer
        playerVFX.ShowComebackVFX(); // VFX
        RestoreHealth(); // Restore HP
    }

    /// <summary>
    /// Restore 50% of lost health
    /// </summary>
    private void RestoreHealth()
    {
        float lostHp = playerHealth.GetLostHealth();
        playerHealth.Heal(lostHp * skillData.effectPercent);
    }
}
