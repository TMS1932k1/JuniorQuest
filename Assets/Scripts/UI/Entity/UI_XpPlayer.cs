using UnityEngine;
using UnityEngine.UI;

public class UI_XpPlayer : MonoBehaviour
{
    [SerializeField] Player_XP playerXP;


    private Slider xpSlider;


    protected virtual void Awake()
    {
        xpSlider = GetComponentInChildren<Slider>();
    }

    protected virtual void Update()
    {
        UpdateHealthSlider();
    }

    protected void UpdateHealthSlider()
    {
        if (xpSlider != null)
            xpSlider.value = playerXP.GetXPPercent();
    }
}
