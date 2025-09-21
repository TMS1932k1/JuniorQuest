using UnityEngine;
using UnityEngine.UI;

public class UI_InstalledSkills : MonoBehaviour
{
    [SerializeField] Player_SkillsManager skillsManager;
    [SerializeField] UI_Slot[] slots;


    private void Start()
    {
        SetDisplay();
    }

    private void Update()
    {
        if (skillsManager.changeInstall)
        {
            SetDisplay();
            skillsManager.changeInstall = false;
        }

        foreach (Skill_Base skill in skillsManager.installedList)
        {
            if (skill.GetCurrentCooldown() > 0)
                slots[skillsManager.installedList.IndexOf(skill)].SetFillAmount(skill.GetCooldownPercent());
        }
    }

    private void SetDisplay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < skillsManager.installedList.Count)
            {
                slots[i].SetDisplaySlot(skillsManager.installedList[i].skillData.skillImage);
            }
            else
            {
                slots[i].SetDisplaySlot(null);
            }
        }
    }


}
