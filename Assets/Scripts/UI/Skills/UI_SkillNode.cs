using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillNode : MonoBehaviour, IPointerDownHandler
{
    [Header("Skill Data")]
    [SerializeField] SkillDataSO skillData;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image skillImage;
    [SerializeField] UI_SkillInfo skillInfoUI;
    [SerializeField] Image installedBorder;


    [SerializeField] Player_XP playerXP;
    [SerializeField] UI_SkillNode conflictSkillUI;


    [SerializeField] Color lockColor;
    [SerializeField] bool isLocked;
    [SerializeField] bool isUnlocked;
    [SerializeField] bool isInstalled;
    public bool needUpdate;


    void OnEnable()
    {
        CheckEnoughLevel();
        // Check installed
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateDisplaySkillInfo();
    }

    void Update()
    {
        if (needUpdate)
        {
            UpdateDisplayNode();
            needUpdate = false;
        }
    }

    public void InstallSkill()
    {
        isInstalled = true;

        // Add to Player_SkillManager

        UpdateDisplaySkillInfo();
        UpdateConflictSkillUI(false);
        needUpdate = true;
    }

    public void UninstallSkill()
    {
        isInstalled = false;

        // Remove to Player_SkillManager

        UpdateDisplaySkillInfo();
        UpdateConflictSkillUI(true);
        needUpdate = true;
    }

    public void CheckEnoughLevel()
    {
        isUnlocked = playerXP.GetLevel() >= skillData.unlockLevel;
        needUpdate = true;
    }

    /// <summary>
    /// Get the status of the skill 
    ///     - Not enough Level -> Locked
    ///     - Installed conflict skills -> Locked
    ///     - Installed -> Installed
    ///     - other -> Unlocked
    /// </summary>
    /// <param name="mes">String to set (statusText) in (UI_SkillInfo)</param>
    /// <returns></returns>
    private SkillStatus GetSkillStatus(out string mes)
    {
        mes = null;

        // Not enough level
        if (!isUnlocked)
        {
            mes = $"At level {skillData.unlockLevel}";
            return SkillStatus.Locked;
        }

        // Installed conflict skills
        if (isLocked)
        {
            mes = $"Installed conflict skills";
            return SkillStatus.Locked;
        }

        // Already installed
        if (isInstalled)
        {
            return SkillStatus.Installed;
        }

        return SkillStatus.Unlocked;
    }

    /// <summary>
    /// Update UI_SkillInfo
    /// </summary>
    private void UpdateDisplaySkillInfo()
    {
        skillInfoUI.DisplayInfo(skillData, GetSkillStatus(out string mes), mes, InstallSkill, UninstallSkill);
    }

    /// <summary>
    /// Update UI_Node
    /// </summary>
    private void UpdateDisplayNode()
    {
        // Update color
        if (isUnlocked || isInstalled)
            skillImage.color = Color.white;
        else
            skillImage.color = lockColor;

        installedBorder.enabled = isInstalled;
    }

    /// <summary>
    /// Update status of conflict skill (Lock skill and update UI)
    /// </summary>
    private void UpdateConflictSkillUI(bool enabled)
    {
        if (conflictSkillUI == null)
            return;

        conflictSkillUI.isLocked = !enabled;
        conflictSkillUI.needUpdate = true;
    }

    void OnValidate()
    {
        if (skillData == null)
            return;

        nameText.text = skillData.skillName;
        skillImage.sprite = skillData.skillImage;

        gameObject.name = "Skill_" + skillData.skillName.Replace(" ", "");
    }
}
