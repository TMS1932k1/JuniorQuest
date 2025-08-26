using UnityEngine;

[CreateAssetMenu(fileName = "SkillData_New", menuName = "Create new Skill Data")]
public class SkillDataSO : ScriptableObject
{
    [Header("Skill details")]
    public Sprite skillImage;
    public string skillName;
    public string description;
    public int unlockLevel;
    public float cooldown;
    public float duration;
    public float damage;
    public float effectPercent;
}
