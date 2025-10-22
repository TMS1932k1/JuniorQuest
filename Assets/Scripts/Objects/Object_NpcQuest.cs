using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Object_NpcQuest : MonoBehaviour, IActive, ISaveable
{
    public static event Action<string> OnCheckQuest;


    [SerializeField] string idQuestTarget;
    [SerializeField] Sprite avtImage;
    [SerializeField] List<Quest> quests = new();
    [TextArea]
    [SerializeField] string[] defaultDialogue;

    [Space]
    [SerializeField] QuestSO questToDisplay;
    [SerializeField] QuestSO questToHide;


    private TextMeshProUGUI tooltipText;
    private bool needUpdate;


    private void Awake()
    {
        tooltipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // If have condition to display
        if (questToDisplay != null || questToHide != null)
        {
            gameObject.SetActive(false);
            return;
        }

        needUpdate = true;
    }

    private void Update()
    {
        if (needUpdate)
        {
            needUpdate = false;
            tooltipText.text = !IsFullTaked() ? "Quest" : "Talk";
        }
    }

    private bool IsFullTaked()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.isTaked)
                return false;
        }

        return true;
    }

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
        needUpdate = true;
        UI_Controller.instance.ShowDialogueUI(quest.questData, avtImage);
    }

    public void SaveData(ref GameData gameData)
    {
        foreach (Quest quest in quests)
        {
            if (!gameData.allQuestStatus.ContainsKey(quest.questData.saveID))
            {
                Debug.Log($"SAVE_MANAGER: Save {quest.questData.questName} ({quest.questData.saveID})");
                gameData.allQuestStatus.Add(quest.questData.saveID, quest.isTaked);
            }
            else
            {
                Debug.Log($"SAVE_MANAGER: Update {quest.questData.questName} ({quest.questData.saveID})");
                gameData.allQuestStatus[quest.questData.saveID] = quest.isTaked;
            }
        }
    }

    public void LoadData(GameData gameData)
    {
        foreach (Quest quest in quests)
        {
            Debug.Log($"SAVE_MANAGER: Load {quest.questData.questName} ({quest.questData.saveID})");

            if (!gameData.allQuestStatus.ContainsKey(quest.questData.saveID))
                continue;

            quest.isTaked = gameData.allQuestStatus[quest.questData.saveID];
        }

        if (questToHide != null
            && gameData.allQuestStatus.ContainsKey(questToHide.saveID)
            && gameData.allQuestStatus[questToHide.saveID]
        )
        {
            gameObject.SetActive(false);
            return;
        }
        else if (questToDisplay != null
            && gameData.allQuestStatus.ContainsKey(questToDisplay.saveID)
            && gameData.allQuestStatus[questToDisplay.saveID])
        {
            gameObject.SetActive(true);
        }

        needUpdate = true;
    }
}
