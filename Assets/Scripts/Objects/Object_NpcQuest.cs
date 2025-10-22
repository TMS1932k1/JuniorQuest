using System;
using System.Collections.Generic;
using UnityEngine;

public class Object_NpcQuest : MonoBehaviour, IActive, ISaveable
{
    public static event Action<string> OnCheckQuest;


    [SerializeField] string idQuestTarget;
    [SerializeField] Sprite avtImage;
    [SerializeField] List<Quest> quests = new();
    [TextArea]
    [SerializeField] string[] defaultDialogue;


    public void Active()
    {
        // Check player had quest
        if (GameManager.instance.GetCurrentQuest != null)
        {
            Debug.Log("Player has mission!");
        }
        else
        {
            foreach (Quest quest in quests)
            {
                if (quest.isTaked)
                    continue;

                HandleTakeQuest(quest);
                return;
            }
        }

        // Show default dialogue
        UI_Controller.instance.ShowDialogueUI(defaultDialogue, avtImage);
        OnCheckQuest?.Invoke(idQuestTarget);
    }

    private void HandleTakeQuest(Quest quest)
    {
        quest.isTaked = true;
        UI_Controller.instance.ShowDialogueUI(quest.questData, avtImage);
    }

    public void SaveData(ref GameData gameData)
    {
        foreach (Quest quest in quests)
        {
            if (!gameData.questStatus.ContainsKey(quest.questData.saveID))
            {
                Debug.Log($"SAVE_MANAGER: Save {quest.questData.questName} ({quest.questData.saveID})");
                gameData.questStatus.Add(quest.questData.saveID, quest.isTaked);
            }
            else
            {
                Debug.Log($"SAVE_MANAGER: Update {quest.questData.questName} ({quest.questData.saveID})");
                gameData.questStatus[quest.questData.saveID] = quest.isTaked;
            }
        }
    }

    public void LoadData(GameData gameData)
    {
        foreach (Quest quest in quests)
        {
            Debug.Log($"SAVE_MANAGER: Load {quest.questData.questName} ({quest.questData.saveID})");

            if (!gameData.questStatus.ContainsKey(quest.questData.saveID))
                continue;

            quest.isTaked = gameData.questStatus[quest.questData.saveID];
        }
    }
}
