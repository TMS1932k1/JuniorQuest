using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Image cooldown;

    public void SetDisplaySlot(Sprite sprite)
    {
        image.enabled = sprite != null;
        image.sprite = sprite;

        cooldown.enabled = sprite != null;
        cooldown.sprite = sprite;
        cooldown.fillAmount = 0;
    }

    public void SetFillAmount(float fillAmount)
    {
        cooldown.fillAmount = fillAmount;
    }
}
