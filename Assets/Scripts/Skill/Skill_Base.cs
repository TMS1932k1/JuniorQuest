using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public SkillDataSO skillData;

    protected Player_VFX playerVFX;

    public bool isInstall;
    public float lastTimeUsed;

    protected virtual void Awake()
    {
        playerVFX = GetComponentInParent<Player_VFX>();

        // Avoid cooldown when start (Time.time = 0)
        lastTimeUsed = -skillData.cooldown;
    }

    public bool CanBeUse()
    {
        // Check installed
        if (!isInstall)
            return false;

        // Check cooldown
        if (GetCurrentCooldown() > 0)
            return false;

        return true;
    }

    public void SetLastTimeUsed()
    {
        lastTimeUsed = Time.time;
    }

    public float GetCurrentCooldown()
    {
        return Mathf.Abs(Mathf.Min(Time.time - lastTimeUsed - skillData.cooldown, 0));
    }

    public float GetCooldownPercent()
    {
        return GetCurrentCooldown() / skillData.cooldown;
    }

    public virtual void PerformSkill()
    {
        SetLastTimeUsed(); // Cooldowntimer
    }
}
