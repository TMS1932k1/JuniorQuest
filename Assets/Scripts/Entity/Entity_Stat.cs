using UnityEngine;

public class Entity_Stat : MonoBehaviour
{
    [SerializeField] private Stat_MajorGroup major;
    [SerializeField] private Stat_OffensiveGroup offensive;
    [SerializeField] private Stat_DefenceGroup defence;

    private float limitEvasion = 85f;

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

    public float GetDamage()
    {
        float damage = offensive.damage.GetValue();
        float strength = major.strength.GetValue();

        return damage + strength; // Per point of strength = 1 damage
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
}
