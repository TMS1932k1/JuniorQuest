using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_SkillsManager : MonoBehaviour
{
    public static event Action OnChangeInstall;

    [Header("Installed details")]
    public int countLimit;
    public List<Skill_Base> installedList { get; private set; } = new();


    // Skills
    public Skill_WindBlade windBlade { get; private set; }
    public Skill_Comeback comeback { get; private set; }
    public Skill_ShieldBarrier shieldBarrier { get; private set; }
    public Skill_Infeno infeno { get; private set; }
    public Skill_IcePrison icePrison { get; private set; }
    public Skill_BattleCry battleCry { get; private set; }
    public Skill_Invisibility invisibility { get; private set; }


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
            OnChangeInstall.Invoke();
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
            OnChangeInstall.Invoke();
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

            case SkillType.None:
            default:
                {
                    Debug.Log("Skill type isn't enable");
                    return null;
                }
        }
    }

    /// <summary>
    /// Handle input by alpha number to use skill
    ///     - Slot 1 = Alpha number 1
    ///     - Slot 2 = Alpha number 2
    ///     - Slot 3 = Alpha number 3
    ///     - Slot 4 = Alpha number 4
    /// </summary>
    /// <returns></returns>
    public SkillType HanldeInputUseSkill()
    {
        SkillType type = SkillType.None;

        for (int i = 0; i < installedList.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                type = GetInstalledSkillType(i);
                break;
            }
        }

        return type;
    }

    private SkillType GetInstalledSkillType(int index)
    {
        if (index >= installedList.Count)
            return SkillType.None;

        return installedList[index].skillData.skillType;
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
