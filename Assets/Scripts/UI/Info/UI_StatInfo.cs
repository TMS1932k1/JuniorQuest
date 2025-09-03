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
        strengthText.text = $"{Stat_Type.Strength}:\t{stat.GetStatWithType(Stat_Type.Strength).GetValue()}";
        agilityText.text = $"{Stat_Type.Agility}:\t{stat.GetStatWithType(Stat_Type.Agility).GetValue()}";
        vitalityText.text = $"{Stat_Type.Vitality}:\t{stat.GetStatWithType(Stat_Type.Vitality).GetValue()}";

        // Offensive
        damageText.text = stat.GetDamage().ToString();
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
        DisplayBuffsWithType(Stat_Type.Damage);
        DisplayBuffsWithType(Stat_Type.CritChance);
        DisplayBuffsWithType(Stat_Type.CritPower);
        DisplayBuffsWithType(Stat_Type.MaxHealth);
        DisplayBuffsWithType(Stat_Type.Armor);
        DisplayBuffsWithType(Stat_Type.Evasion);
        DisplayBuffsWithType(Stat_Type.Mitigation);
    }

    /// <summary>
    /// Get text of buffs with type (Example: "Apple HP: +5 MaxHealth")
    /// Add this to (buffsText)
    /// </summary>
    /// <param name="type">Stat type which need get modifiers</param>
    private void DisplayBuffsWithType(Stat_Type type)
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

    private string GetTextWithType(Stat_Type type)
    {
        switch (type)
        {
            // Major Group
            case Stat_Type.Strength: return "Strength";
            case Stat_Type.Agility: return "Agility";
            case Stat_Type.Vitality: return "Vitality";

            // Offensive Group
            case Stat_Type.Damage: return "Damage";
            case Stat_Type.CritChance: return "Crit Chance";
            case Stat_Type.CritPower: return "Crit Power";

            // Defence Group
            case Stat_Type.MaxHealth: return "Max Health";
            case Stat_Type.Evasion: return "Evasion";
            case Stat_Type.Armor: return "Armor";
            case Stat_Type.Mitigation: return "Mitigation";

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
