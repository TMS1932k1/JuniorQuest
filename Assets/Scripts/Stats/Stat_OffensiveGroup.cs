using UnityEngine;

[System.Serializable]
public class Stat_OffensiveGroup
{
    public Stat damage;
    public Stat critPower;
    public Stat critChance;

    public void AddAllModifierWithPercent(string source, float percent)
    {
        damage.AddModifier(source, damage.GetValue() * percent);
        critChance.AddModifier(source, critChance.GetValue() * percent);
        critPower.AddModifier(source, critPower.GetValue() * percent);
    }

    public void RemoveAllModifierWithPercent(string source)
    {
        damage.RemoveModifier(source);
        critChance.RemoveModifier(source);
        critPower.RemoveModifier(source);
    }
}
