using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class TargetGoal
{
    public string idQuesTarget;
    public int count;
}


[CreateAssetMenu(menuName = "Quest Setup/Create new Quest Data", fileName = "Quest_New")]
public class QuestSO : ScriptableObject
{
    public string saveID;
    public string questName;
    public string description;
    [TextArea] public string[] dialoges;
    public List<TargetGoal> targetGoals;


    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        saveID = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
