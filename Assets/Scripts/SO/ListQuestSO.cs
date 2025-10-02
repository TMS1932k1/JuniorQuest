using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest Setup/Create new ListQuest Data", fileName = "QUEST_LIST")]
public class ListQuestSO : ScriptableObject
{
    public QuestSO[] questList;


    public QuestSO GetPickUpWithSaveID(string saveID)
    {
        return questList.FirstOrDefault(quest => quest != null && quest.saveID == saveID);
    }
}
