using UnityEngine;

public class Player_Mission : MonoBehaviour, ISaveable
{
    private Quest quest;
    [SerializeField] ListQuestSO listQuestSO;


    private void Awake()
    {
        quest = null;
    }

    public bool IsEmptyMission() => quest == null;

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save Mission of Player");
        gameData.questId = quest != null ? quest.questData.saveID : null;
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load Mission of Player");

        if (string.IsNullOrEmpty(gameData.questId))
            return;

        QuestSO questData = listQuestSO.GetPickUpWithSaveID(gameData.questId);
        if (questData == null)
        {
            Debug.LogWarning($"Found not mission with this ID");
            return;
        }

        quest = new Quest(questData, false);
    }
}
