using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public SkillSO skillData;

    protected Player_VFX playerVFX;
    protected Player_SFX playerSFX;
    protected Entity_Stat stat;


    public bool isInstall;
    public float lastTimeUsed;


    protected virtual void Awake()
    {
        playerVFX = GetComponentInParent<Player_VFX>();
        playerSFX = GetComponentInParent<Player_SFX>();
        stat = GetComponentInParent<Entity_Stat>();

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

    protected virtual void CalcullateDamage(out float finalDamage)
    {
        finalDamage = 0;
    }
}
