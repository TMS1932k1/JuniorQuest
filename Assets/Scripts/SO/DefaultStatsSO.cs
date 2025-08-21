using UnityEngine;

[CreateAssetMenu(menuName = "DefaultStats Scriptable Object", fileName = "New_DefaultStats")]
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

}
