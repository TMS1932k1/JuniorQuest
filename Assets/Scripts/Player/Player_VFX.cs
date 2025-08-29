using System.Collections;
using UnityEngine;

public class Player_VFX : Entity_VFX
{
    [Header("Level Up")]
    [SerializeField] UI_LevelUp lvUpUI;
    [SerializeField] float durationShow;
    [SerializeField] float showSpeed = 1f;


    [Header("Comeback skill")]
    [SerializeField] GameObject comebackVFX;

    [Header("Invisibility skill")]
    [SerializeField] GameObject invisibilityVFX;

    [Header("Shield Barrier skill")]
    [SerializeField] GameObject shieldBarrierVFX;


    public void ShowLevelUpVFX(int level)
    {
        lvUpUI.ShowText(level, durationShow, showSpeed);
    }

    public void ShowComebackVFX()
    {
        comebackVFX.SetActive(true);
        Invoke(nameof(HideComebackVFX), 1f);
    }

    private void HideComebackVFX()
    {
        comebackVFX.SetActive(false);
    }

    public void ShowInvisibilityVFX()
    {
        invisibilityVFX.SetActive(true);
        Invoke(nameof(HideInvisibilityVFX), 1f);
    }

    private void HideInvisibilityVFX()
    {
        invisibilityVFX.SetActive(false);
    }

    public void ShowShieldBarrierVFX(float durationShow)
    {
        shieldBarrierVFX.SetActive(true);
        Invoke(nameof(HideShieldBarrierVFX), durationShow);
    }

    private void HideShieldBarrierVFX()
    {
        shieldBarrierVFX.SetActive(false);
    }

    public void SetFadePlayer(float fadePercent)
    {
        Color c = sr.color;
        c.a = fadePercent;
        sr.color = c;
    }
}
