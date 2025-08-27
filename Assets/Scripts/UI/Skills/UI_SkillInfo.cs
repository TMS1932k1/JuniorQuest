using System;
using TMPro;
using UnityEngine;

public class UI_SkillInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI installText;
    [SerializeField] TextMeshProUGUI uninstallText;


    private SkillDataSO skillData;
    private ISkillInfoEvent infoEvent;
    private SkillStatus status;
    private bool needUpdate;


    void Update()
    {
        UpdateSize();

        if (needUpdate)
        {
            UpdateInfo();
            needUpdate = false;
        }
    }

    private void UpdateSize()
    {
        if (skillData == null)
            transform.localScale = Vector3.zero;
        else
            transform.localScale = Vector3.one;
    }

    public void DisplayInfo(SkillDataSO skillData, SkillStatus status, string statusMes, ISkillInfoEvent infoEvent)
    {
        this.skillData = skillData;
        this.status = status;
        statusText.text = statusMes;

        this.infoEvent = infoEvent;

        needUpdate = true;
    }

    public void InstallSkill()
    {
        if (infoEvent == null)
            return;

        infoEvent.InstallSkill();
    }

    public void UninstallSkill()
    {
        if (infoEvent == null)
            return;

        infoEvent.UninstallSkill();
    }

    private void UpdateInfo()
    {
        if (skillData == null)
            return;

        nameText.text = skillData.skillName;
        descriptionText.text = skillData.description;

        installText.enabled = status == SkillStatus.Unlocked;
        uninstallText.enabled = status == SkillStatus.Installed;
        statusText.enabled = status == SkillStatus.Locked;
    }
}
