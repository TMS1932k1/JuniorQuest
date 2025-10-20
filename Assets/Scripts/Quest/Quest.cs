using UnityEngine;

[System.Serializable]
public class Quest
{
    public QuestSO questData;
    public bool isTaked;

    public Quest(QuestSO questData, bool isTaked = false)
    {
        this.questData = questData;
        this.isTaked = isTaked;
    }
}
