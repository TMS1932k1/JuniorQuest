using System;
using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [SerializeField] protected SkillDataSO skillData;

    public bool isInstall;
    public float lastTimeUsed;


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

    public float GetCurrentCooldown()
    {
        return Mathf.Abs(Mathf.Min(Time.time - lastTimeUsed - skillData.cooldown, 0));
    }
}
