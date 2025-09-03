using UnityEngine;

[CreateAssetMenu(fileName = "SkillData_New", menuName = "Create new Skill Data")]
public class SkillDataSO : ScriptableObject
{
    [Header("Skill details")]
    public Skill_Type skillType;
    public int unlockLevel;
    public float cooldown;
    public float duration;
    public float damage;
    public float effectPercent;
    public float effectDuration;
    public int countHit;


    [Header("Display")]
    public Sprite skillImage;
    public string skillName;
    public string description;


    [Header("Arena details")]
    public float radiusArena;
    public float widthArena;
    public float heightArena;
}
