using UnityEngine;

public class Player_SkillsManager : MonoBehaviour
{
    [SerializeField] Skill_WindBlade windBlade;
    [SerializeField] Skill_Comeback comeback;

    public void InstallSkill(SkillType type, out bool success)
    {
        Skill_Base installSkill = GetSkillWithType(type);

        if (installSkill)
        {
            success = true;
            installSkill.isInstall = true;
        }
        else
        {
            success = false;
        }
    }

    public void UnnstallSkill(SkillType type, out bool success)
    {
        Skill_Base installSkill = GetSkillWithType(type);

        if (installSkill)
        {
            success = true;
            installSkill.isInstall = true;
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

            default:
                {
                    Debug.Log("Skill type isn't enable");
                    return null;
                }
        }
    }
}
