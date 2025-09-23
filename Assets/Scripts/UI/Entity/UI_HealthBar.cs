using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    protected Entity_Health entityHealth;
    protected Slider healthSlider;

    protected virtual void Awake()
    {
        entityHealth = GetComponentInParent<Entity_Health>();
        healthSlider = GetComponentInChildren<Slider>();
    }

    protected virtual void Update()
    {
        transform.rotation = Quaternion.identity;
        UpdateHealthSlider();
    }

    protected void UpdateHealthSlider()
    {
        if (healthSlider != null)
            healthSlider.value = entityHealth.GetHealthPercent();
    }
}
