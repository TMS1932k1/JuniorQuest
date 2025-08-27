using System.Collections.Generic;
using UnityEngine;

public class Player_SkillsManager : MonoBehaviour
{
    [Header("Installed details")]
    [SerializeField] int countLimit;
    private List<Skill_Base> installedList = new();


    [Header("Skills")]
    private Skill_WindBlade windBlade;
    private Skill_Comeback comeback;
    private Skill_ShieldBarrier shieldBarrier;
    private Skill_Infeno infeno;
    private Skill_IcePrison icePrison;
    private Skill_BattleCry battleCry;
    private Skill_Invisibility invisibility;


    void Awake()
    {
        windBlade = GetComponentInChildren<Skill_WindBlade>();
        comeback = GetComponentInChildren<Skill_Comeback>();
        shieldBarrier = GetComponentInChildren<Skill_ShieldBarrier>();
        infeno = GetComponentInChildren<Skill_Infeno>();
        icePrison = GetComponentInChildren<Skill_IcePrison>();
        battleCry = GetComponentInChildren<Skill_BattleCry>();
        invisibility = GetComponentInChildren<Skill_Invisibility>();
    }

    public void InstallSkill(SkillType type, out bool success)
    {
        Skill_Base skill = GetSkillWithType(type);

        if (skill && !IsFullSlot()) // Check have slot to install skill
        {
            skill.isInstall = true;
            installedList.Add(skill);
            success = true;
        }
        else
        {
            success = false;
        }
    }

    public void UninstallSkill(SkillType type, out bool success)
    {
        Skill_Base skill = GetSkillWithType(type);

        if (skill)
        {
            installedList.Remove(skill);
            skill.isInstall = false;
            success = true;
        }
        else
        {
            success = false;
        }
    }

    /// <summary>
    /// Get skill with (SkillType)
    /// </summary>
    /// <param name="type">Enum skill type to found</param>
    /// <returns>Skill need get</returns>
    public Skill_Base GetSkillWithType(SkillType type)
    {
        switch (type)
        {
            case SkillType.WindBlade:
                return windBlade;

            case SkillType.Comeback:
                return comeback;

            case SkillType.ShieldBarrier:
                return shieldBarrier;

            case SkillType.IcePrison:
                return icePrison;

            case SkillType.Infeno:
                return infeno;

            case SkillType.BattleCry:
                return battleCry;

            case SkillType.Invisibility:
                return invisibility;

            default:
                {
                    Debug.Log("Skill type isn't enable");
                    return null;
                }
        }
    }

    public bool IsInstalled(SkillType type)
    {
        return GetSkillWithType(type)?.isInstall ?? false;
    }

    public bool IsFullSlot()
    {
        return installedList.Count >= countLimit;
    }
}
