using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Data Setup/Create new Skill Data", fileName = "SkillData_New")]
public class SkillSO : ScriptableObject
{
    [Header("Skill details")]
    public string saveID;
    public ESkill_Type skillType;
    public int unlockLevel;
    public float cooldown;
    public float duration;
    public float damage;
    public float effectPercent;
    public float effectDuration;
    public int hitCount;


    [Header("Display")]
    public Sprite skillImage;
    public string skillName;
    public string description;


    [Header("Arena details")]
    public float radiusArena;
    public float widthArena;
    public float heightArena;

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        saveID = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
