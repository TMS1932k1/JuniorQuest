using UnityEngine;

[System.Serializable]
public class Stat_DefenceGroup
{
    public Stat maxHealth;
    public Stat evasion;
    public Stat armor;
    public Stat mitigation;

    public void AddAllModifierWithPercent(string source, float percent)
    {
        armor.AddModifier(source, armor.GetValue() * percent);
        maxHealth.AddModifier(source, maxHealth.GetValue() * percent);
        evasion.AddModifier(source, evasion.GetValue() * percent);
        mitigation.AddModifier(source, mitigation.GetValue() * percent);
    }

    public void RemoveAllModifierWithPercent(string source)
    {
        armor.RemoveModifier(source);
        maxHealth.RemoveModifier(source);
        evasion.RemoveModifier(source);
        mitigation.RemoveModifier(source);
    }
}
