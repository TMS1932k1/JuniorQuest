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


    public void ShowLevelUpVFX(int level)
    {
        lvUpUI.ShowText(level, durationShow, showSpeed);
    }

    public void ShowComebackVFX()
    {
        GameObject vfx = Instantiate(comebackVFX, transform);
        Destroy(vfx, 1f);
    }

    public void ShowInvisibilityVFX()
    {
        GameObject vfx = Instantiate(invisibilityVFX, transform);
        Destroy(vfx, 1f);
    }

    public void SetFadePlayer(float fadePercent)
    {
        Color c = sr.color;
        c.a = fadePercent;
        sr.color = c;
    }
}
