using UnityEngine;

public class Entity_Stat : MonoBehaviour
{
    [SerializeField] DefaultStatsSO data;


    [SerializeField] Stat_MajorGroup major;
    [SerializeField] Stat_OffensiveGroup offensive;
    [SerializeField] Stat_DefenceGroup defence;
    [SerializeField] float xp;

    private float limitEvasion = 85f;


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

        return Mathf.Clamp(evasion + agility * 0.5f, 0, limitEvasion); // Per point of agility = 0.5 evasion, result not bigger limit
    }

    public float GetBaseDamageNoCrit()
    {
        float damage = offensive.damage.GetValue();
        float strength = major.strength.GetValue();

        return damage + strength; // Per point of strength = 1 damage
    }

    public float GetDamage(out bool isCrit)
    {
        float damage = offensive.damage.GetValue();
        float strength = major.strength.GetValue();
        float sumDamage = damage + strength; // Per point of strength = 1 damage

        isCrit = IsCrit();
        return sumDamage * (isCrit ? (1 + GetCritPower() / 100) : 1);
    }

    private bool IsCrit()
    {
        return Random.Range(0, 100) <= GetCritChange();
    }

    public float GetCritPower()
    {
        float critPower = offensive.critPower.GetValue();
        float strength = major.strength.GetValue();

        return critPower + strength * 0.5f; // Per point of strength = 0.5 crit power
    }

    public float GetCritChange()
    {
        float critChance = offensive.critChance.GetValue();
        float agility = major.agility.GetValue();

        return critChance + agility * 0.3f; // Per point of agility = 0.3 crit chance
    }

    public float GetArmor()
    {
        float armor = defence.armor.GetValue();
        float vitality = major.vitality.GetValue();

        return armor + vitality; // Per point of vitality = 1 armor
    }

    public float GetMitigation()
    {
        return Mathf.Clamp01(defence.mitigation.GetValue() / 100f);
    }

    public Stat GetStatWithType(StatType type)
    {
        switch (type)
        {
            // Major Group
            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Vitality: return major.vitality;
            // Offensive Group
            case StatType.Damage: return offensive.damage;
            case StatType.CritChance: return offensive.critChance;
            case StatType.CritPower: return offensive.critPower;
            // Defence Group
            case StatType.MaxHealth: return defence.maxHealth;
            case StatType.Evasion: return defence.evasion;
            case StatType.Armor: return defence.armor;
            case StatType.Mitigation: return defence.mitigation;

            default:
                {
                    Debug.Log("Don't have this stat");
                    return null;
                }
        }
    }

    public void AddAllModifierWithPercent(string source, float percent)
    {
        offensive.AddAllModifierWithPercent(source, percent);
        defence.AddAllModifierWithPercent(source, percent);
    }

    public void RemoveAllModifierWithPercent(string source)
    {
        offensive.RemoveAllModifierWithPercent(source);
        defence.RemoveAllModifierWithPercent(source);
    }

    public float GetXp()
    {
        return xp;
    }
}
