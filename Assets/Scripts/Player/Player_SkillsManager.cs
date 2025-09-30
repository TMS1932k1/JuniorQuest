using System.Collections.Generic;
using UnityEngine;

public class Player_SkillsManager : MonoBehaviour, ISaveable
{
    [Header("Installed details")]
    public int countLimit;
    public List<Skill_Base> installedList { get; private set; } = new();
    public bool changeInstall = false;


    // Skills
    public Skill_FireBlade fireBlade { get; private set; }
    public Skill_Comeback comeback { get; private set; }
    public Skill_ShieldBarrier shieldBarrier { get; private set; }
    public Skill_Infeno infeno { get; private set; }
    public Skill_IcePrison icePrison { get; private set; }
    public Skill_BattleCry battleCry { get; private set; }
    public Skill_Invisibility invisibility { get; private set; }


    private Player player;


    void Awake()
    {
        fireBlade = GetComponentInChildren<Skill_FireBlade>();
        comeback = GetComponentInChildren<Skill_Comeback>();
        shieldBarrier = GetComponentInChildren<Skill_ShieldBarrier>();
        infeno = GetComponentInChildren<Skill_Infeno>();
        icePrison = GetComponentInChildren<Skill_IcePrison>();
        battleCry = GetComponentInChildren<Skill_BattleCry>();
        invisibility = GetComponentInChildren<Skill_Invisibility>();

        player = GetComponent<Player>();
    }

    public void InstallSkill(ESkill_Type type, out bool success)
    {
        Skill_Base skill = GetSkillWithType(type);

        if (skill && !IsFullSlot()) // Check have slot to install skill
        {
            HandleAddSkill(skill);
            success = true;
        }
        else
        {
            success = false;
        }
    }

    private void HandleAddSkill(Skill_Base skill)
    {
        installedList.Add(skill);
        skill.isInstall = true;
        changeInstall = true;
    }

    public void UninstallSkill(ESkill_Type type, out bool success)
    {
        Skill_Base skill = GetSkillWithType(type);

        if (skill)
        {
            HandlRemoveSkill(skill);
            success = true;
        }
        else
        {
            success = false;
        }
    }

    private void HandlRemoveSkill(Skill_Base skill)
    {
        installedList.Remove(skill);
        skill.isInstall = false;
        changeInstall = true;
    }

    /// <summary>
    /// Get skill with (SkillType)
    /// </summary>
    /// <param name="type">Enum skill type to found</param>
    /// <returns>Skill need get</returns>
    public Skill_Base GetSkillWithType(ESkill_Type type)
    {
        switch (type)
        {
            case ESkill_Type.FireBlade:
                return fireBlade;

            case ESkill_Type.Comeback:
                return comeback;

            case ESkill_Type.ShieldBarrier:
                return shieldBarrier;

            case ESkill_Type.IcePrison:
                return icePrison;

            case ESkill_Type.Infeno:
                return infeno;

            case ESkill_Type.BattleCry:
                return battleCry;

            case ESkill_Type.Invisibility:
                return invisibility;

            case ESkill_Type.None:
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
    public ESkill_Type HanldeInputUseSkill()
    {
        if (player.input.Player.Skill1.WasPressedThisFrame())
            return GetInstalledSkillType(0);

        if (player.input.Player.Skill2.WasPressedThisFrame())
            return GetInstalledSkillType(1);

        if (player.input.Player.Skill3.WasPressedThisFrame())
            return GetInstalledSkillType(2);

        if (player.input.Player.Skill4.WasPressedThisFrame())
            return GetInstalledSkillType(3);

        return ESkill_Type.None;
    }

    private ESkill_Type GetInstalledSkillType(int index)
    {
        if (index >= installedList.Count)
            return ESkill_Type.None;

        return installedList[index].skillData.skillType;
    }

    public bool IsInstalled(ESkill_Type type)
    {
        return GetSkillWithType(type)?.isInstall ?? false;
    }

    public bool IsFullSlot()
    {
        return installedList.Count >= countLimit;
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save Installed Skills of Player");

        gameData.installedSkills.Clear();

        foreach (Skill_Base skill in installedList)
            gameData.installedSkills.Add(skill.skillData.saveID);
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load Installed Skills of Player");

        HandleOldData();

        foreach (string saveID in gameData.installedSkills)
        {
            Skill_Base skill = GetSkillBySaveID(saveID);

            if (skill == null && IsFullSlot())
                continue;

            HandleAddSkill(skill);
        }

        changeInstall = true;
    }

    private void HandleOldData()
    {
        installedList.Clear();

        fireBlade.isInstall = false;
        comeback.isInstall = false;
        shieldBarrier.isInstall = false;
        icePrison.isInstall = false;
        infeno.isInstall = false;
        battleCry.isInstall = false;
        invisibility.isInstall = false;
    }

    private Skill_Base GetSkillBySaveID(string saveID)
    {
        if (saveID == fireBlade.skillData.saveID)
            return fireBlade;

        if (saveID == comeback.skillData.saveID)
            return comeback;

        if (saveID == shieldBarrier.skillData.saveID)
            return shieldBarrier;

        if (saveID == icePrison.skillData.saveID)
            return icePrison;

        if (saveID == infeno.skillData.saveID)
            return infeno;

        if (saveID == battleCry.skillData.saveID)
            return battleCry;

        if (saveID == invisibility.skillData.saveID)
            return invisibility;

        return null;
    }
}
