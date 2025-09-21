using UnityEngine;

[CreateAssetMenu(menuName = "Buff Data Setup/Create new ObjectBuff Data", fileName = "ObjectBuffData_New")]
public class ObjectBuffDataSO : ScriptableObject
{
    [Header("Buff details")]
    public float duration;
    public EStat_Type statType;
    public string source;
    public float value;
    public Sprite image;
}
