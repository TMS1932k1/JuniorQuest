using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SkillInstall : MonoBehaviour, IPointerDownHandler
{
    private UI_SkillInfo skillInfoUI;

    void Awake()
    {
        skillInfoUI = GetComponentInParent<UI_SkillInfo>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        skillInfoUI.InstallSkill();
    }
}
