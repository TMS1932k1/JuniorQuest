using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity_Health entityHealth;
    private Slider healthSlider;

    void Awake()
    {
        entityHealth = GetComponentInParent<Entity_Health>();
        healthSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        UpdateHealthSlider();
    }

    public void UpdateHealthSlider()
    {
        if (healthSlider != null)
            healthSlider.value = entityHealth.GetHealthPercent();
    }
}
