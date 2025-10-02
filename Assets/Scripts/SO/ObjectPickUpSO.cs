using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Pick Up Setup/Create new ObjectPickUp Data", fileName = "ObjectPickUpData_New")]
public class ObjectPickUpSO : ScriptableObject
{
    public string saveID;
    public Sprite image;
    public string pickUpName;


    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        saveID = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
