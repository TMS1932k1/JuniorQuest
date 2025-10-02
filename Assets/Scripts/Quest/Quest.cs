using UnityEngine;

[System.Serializable]
public class Quest
{
    public Quest(QuestSO questData, bool isCompleted)
    {
        this.questData = questData;
        this.isCompleted = isCompleted;
    }

    public QuestSO questData;
    public bool isCompleted;
}
