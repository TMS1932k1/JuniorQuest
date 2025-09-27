using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifierStat
{
    public string source;
    public float value;

    public ModifierStat(string source, float value)
    {
        this.source = source;
        this.value = value;
    }
}

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private float finalValue;
    [SerializeField] public List<ModifierStat> modifiers { get; private set; } = new();

    private bool isNeedUpdate = true;

    public float GetValue()
    {
        if (isNeedUpdate)
        {
            UpdateFinalValue();
            isNeedUpdate = false;
        }

        return finalValue;
    }

    public void SetValue(float value)
    {
        baseValue = value;
        isNeedUpdate = true;
    }

    public void AddModifier(string source, float value)
    {
        modifiers.Add(new ModifierStat(source, value));
        isNeedUpdate = true;
    }

    public void RemoveModifier()
    {
        modifiers.Clear();
        isNeedUpdate = true;
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(m => m.source == source);
        isNeedUpdate = true;
    }

    private void UpdateFinalValue()
    {
        finalValue = baseValue;
        foreach (ModifierStat modifier in modifiers)
        {
            finalValue += modifier.value;
        }
    }
}