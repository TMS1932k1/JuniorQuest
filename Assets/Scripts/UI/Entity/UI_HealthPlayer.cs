using UnityEngine;
using UnityEngine.UI;

public class UI_HealthPlayer : UI_HealthBar
{
    [SerializeField] private Player_Health playerHealth;
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color mediumColor = Color.yellow;
    [SerializeField] private Color dangerColor = Color.red;

    protected override void Awake()
    {
        entityHealth = playerHealth;
        healthSlider = GetComponentInChildren<Slider>();
    }

    protected override void Update()
    {
        UpdateHealthSlider();
        UpdateColor();
    }

    /// <summary>
    /// Update color of slider
    /// - 100-60% -> Green
    /// - 60-30% -> Yellow
    /// - 30-0% -> Red
    /// </summary>
    private void UpdateColor()
    {
        if (healthSlider != null)
        {
            Image fill = healthSlider.fillRect.GetComponent<Image>();
            float healthPercent = entityHealth.GetHealthPercent();

            if (healthPercent <= 1 && healthPercent > 0.6f)
                fill.color = safeColor;
            else if (healthPercent > 0.3f)
                fill.color = mediumColor;
            else
                fill.color = dangerColor;
        }
    }
}
