using TMPro;
using UnityEngine;

public class UI_StatInfo : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] Entity_Stat stat;


    [Header("Major")]
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI agilityText;
    [SerializeField] TextMeshProUGUI vitalityText;

    [Header("Offensive")]
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI critChanceText;
    [SerializeField] TextMeshProUGUI critPowerText;

    [Header("Defence")]
    [SerializeField] TextMeshProUGUI maxHealthText;
    [SerializeField] TextMeshProUGUI armorText;
    [SerializeField] TextMeshProUGUI evasionText;
    [SerializeField] TextMeshProUGUI mitigationText;

    [Header("Buffs")]
    [SerializeField] TextMeshProUGUI buffsText;


    void Update()
    {
        if (stat.haveChange)
        {
            SetStatsDisplay();
            SetBuffsDisplay();

            stat.haveChange = false;
        }
    }

    private void SetStatsDisplay()
    {
        // Major
        strengthText.text = $"{StatType.Strength}:\t{stat.GetStatWithType(StatType.Strength).GetValue()}";
        agilityText.text = $"{StatType.Agility}:\t{stat.GetStatWithType(StatType.Agility).GetValue()}";
        vitalityText.text = $"{StatType.Vitality}:\t{stat.GetStatWithType(StatType.Vitality).GetValue()}";

        // Offensive
        damageText.text = stat.GetBaseDamageNoCrit().ToString();
        critChanceText.text = stat.GetCritChange().ToString();
        critPowerText.text = stat.GetCritPower().ToString();

        // Defence
        maxHealthText.text = stat.GetHealth().ToString();
        armorText.text = stat.GetArmor().ToString();
        evasionText.text = stat.GetEvasion().ToString();
        mitigationText.text = stat.GetMitigation().ToString();
    }

    private void SetBuffsDisplay()
    {
        // Clear buffs
        buffsText.text = "";
        SetHeightBuffsUI(0);

        // Add buffs
        DisplayBuffsWithType(StatType.Damage);
        DisplayBuffsWithType(StatType.CritChance);
        DisplayBuffsWithType(StatType.CritPower);
        DisplayBuffsWithType(StatType.MaxHealth);
        DisplayBuffsWithType(StatType.Armor);
        DisplayBuffsWithType(StatType.Evasion);
        DisplayBuffsWithType(StatType.Mitigation);
    }

    /// <summary>
    /// Get text of buffs with type (Example: "Apple HP: +5 MaxHealth")
    /// Add this to (buffsText)
    /// </summary>
    /// <param name="type">Stat type which need get modifiers</param>
    private void DisplayBuffsWithType(StatType type)
    {
        foreach (ModifierStat modifier in stat.GetStatWithType(type).modifiers)
        {
            if (modifier.value <= 0)
                continue;

            // Add Text of buff
            string buff = $"- {modifier.source.ToUpper()}: +{modifier.value} {GetTextWithType(type)}\n";
            buffsText.text += buff;

            // Increment height
            SetHeightBuffsUI(buffsText.rectTransform.sizeDelta.y + 25f);
        }
    }

    private string GetTextWithType(StatType type)
    {
        switch (type)
        {
            // Major Group
            case StatType.Strength: return "Strength";
            case StatType.Agility: return "Agility";
            case StatType.Vitality: return "Vitality";

            // Offensive Group
            case StatType.Damage: return "Damage";
            case StatType.CritChance: return "Crit Chance";
            case StatType.CritPower: return "Crit Power";

            // Defence Group
            case StatType.MaxHealth: return "Max Health";
            case StatType.Evasion: return "Evasion";
            case StatType.Armor: return "Armor";
            case StatType.Mitigation: return "Mitigation";

            default:
                return "";
        }
    }

    private void SetHeightBuffsUI(float y)
    {
        Vector2 size = buffsText.rectTransform.sizeDelta;
        size.y = y;
        buffsText.rectTransform.sizeDelta = size;
    }
}
