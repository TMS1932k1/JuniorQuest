using System;
using System.Collections.Generic;
using UnityEngine;

public class Object_NpcQuest : MonoBehaviour, IActive
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
}
