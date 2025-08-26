using UnityEngine;

[CreateAssetMenu(menuName = "Create new DefaultStats", fileName = "DefaultStats_New")]
public class DefaultStatsSO : ScriptableObject
{
    [Header("Major Stats")]
    public float strength;
    public float agility;
    public float vitality;


    [Header("Offensive Stats")]
    public float damage;
    public float critChance;
    public float critPower;


    [Header("Defence Stats")]
    public float maxHealth;
    public float evasion;
    public float armor;


    [Header("XP")]
    public float xp;
}
