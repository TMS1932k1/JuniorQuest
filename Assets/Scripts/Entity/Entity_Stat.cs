using System;
using UnityEngine;

public class Entity_Stat : MonoBehaviour
{
    [SerializeField] DefaultStatsSO data;

    [Header("Stat Groups")]
    [SerializeField] protected Stat_MajorGroup major;
    [SerializeField] Stat_OffensiveGroup offensive;
    [SerializeField] Stat_DefenceGroup defence;
    [SerializeField] float xp;


    private float limitEvasion = 85f;

    public bool haveChange = true;


    void Start()
    {
        SetUpDefaultStats();
    }

    [ContextMenu("Set up default stats")]
    protected virtual void SetUpDefaultStats()
    {
        major.strength.SetValue(data.strength);
        major.agility.SetValue(data.agility);
        major.vitality.SetValue(data.vitality);
        offensive.damage.SetValue(data.damage);
        offensive.critChance.SetValue(data.critChance);
        offensive.critPower.SetValue(data.critPower);
        defence.maxHealth.SetValue(data.maxHealth);
        defence.evasion.SetValue(data.evasion);
        defence.armor.SetValue(data.armor);
        defence.mitigation.SetValue(data.mitigation);
        xp = data.xp;
    }

    public float GetHealth()
    {
        float maxhealth = defence.maxHealth.GetValue();
        float vitality = major.vitality.GetValue();

        return maxhealth + vitality * 5f; // Per point of vitality = 5 maxHealth
    }

    public float GetEvasion()
    {
        float evasion = defence.evasion.GetValue();
        float agility = major.agility.GetValue();

        return Mathf.Clamp(evasion + agility * 1f, 0, limitEvasion); // Per point of agility = 1 evasion, result not bigger limit
    }

    public float GetDamage()
    {
        float damage = offensive.damage.GetValue();
        float strength = major.strength.GetValue();

        return damage + strength * 2f; // Per point of strength = 2 damage
    }

    public float GetDamageWithCrit(out bool isCrit)
    {
        isCrit = IsCrit();
        return GetDamage() * (isCrit ? (1 + GetCritPower() / 100) : 1);
    }

    public float GetCritPower()
    {
        float critPower = offensive.critPower.GetValue();
        float strength = major.strength.GetValue();

        return critPower + strength * 10f; // Per point of strength = 10 crit power
    }

    public float GetCritChange()
    {
        float critChance = offensive.critChance.GetValue();
        float agility = major.agility.GetValue();

        return critChance + agility * 5f; // Per point of agility = 5 crit chance
    }

    public float GetArmor()
    {
        float armor = defence.armor.GetValue();
        float vitality = major.vitality.GetValue();

        return armor + vitality * 3f; // Per point of vitality = 2 armor
    }

    public float GetMitigation()
    {
        return Mathf.Clamp01(defence.mitigation.GetValue() / 100f);
    }

    private bool IsCrit()
    {
        return UnityEngine.Random.Range(0, 100) <= GetCritChange();
    }

    public Stat GetStatWithType(EStat_Type type)
    {
        switch (type)
        {
            // Major Group
            case EStat_Type.Strength: return major.strength;
            case EStat_Type.Agility: return major.agility;
            case EStat_Type.Vitality: return major.vitality;

            // Offensive Group
            case EStat_Type.Damage: return offensive.damage;
            case EStat_Type.CritChance: return offensive.critChance;
            case EStat_Type.CritPower: return offensive.critPower;

            // Defence Group
            case EStat_Type.MaxHealth: return defence.maxHealth;
            case EStat_Type.Evasion: return defence.evasion;
            case EStat_Type.Armor: return defence.armor;
            case EStat_Type.Mitigation: return defence.mitigation;

            default:
                {
                    Debug.Log("Don't have this stat");
                    return null;
                }
        }
    }

    public void AddModifierWithType(EStat_Type type, string source, float value)
    {
        GetStatWithType(type).AddModifier(source, value);
        haveChange = true;
    }

    public void RemoveModifierWithType(EStat_Type type, string source)
    {
        GetStatWithType(type).RemoveModifier(source);
        haveChange = true;
    }

    public void AddAllModifierWithPercent(string source, float percent)
    {
        offensive.AddAllModifierWithPercent(source, percent);
        defence.AddAllModifierWithPercent(source, percent);
        haveChange = true;
    }

    public void RemoveAllModifierWithPercent(string source)
    {
        offensive.RemoveAllModifierWithPercent(source);
        defence.RemoveAllModifierWithPercent(source);
        haveChange = true;
    }

    public void ResetModifier()
    {
        offensive.damage.RemoveModifier();
        offensive.critChance.RemoveModifier();
        offensive.critPower.RemoveModifier();
        defence.maxHealth.RemoveModifier();
        defence.evasion.RemoveModifier();
        defence.armor.RemoveModifier();
        defence.mitigation.RemoveModifier();

        haveChange = true;
    }

    public float GetXp()
    {
        return xp;
    }

    public float GetSumMajorPoint()
    {
        return major.strength.GetValue() + major.agility.GetValue() + major.vitality.GetValue();
    }
}
