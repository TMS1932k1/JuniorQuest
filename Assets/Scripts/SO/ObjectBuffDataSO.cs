using UnityEngine;

[CreateAssetMenu(menuName = "Create new ObjectBuff Data", fileName = "ObjectBuffData_New")]
public class ObjectBuffDataSO : ScriptableObject
{
    [Header("Buff details")]
    public float duration;
    public StatType statType;
    public string source;
    public float value;
    public Sprite image;
}
