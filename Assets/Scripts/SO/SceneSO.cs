using UnityEngine;


[CreateAssetMenu(menuName = "Scene Data Setup/Create new Scene Data", fileName = "SceneData_New")]
public class SceneSO : ScriptableObject
{
    public string sceneName;
    public string normalAudioName;
    public string battleAudioName;
}
