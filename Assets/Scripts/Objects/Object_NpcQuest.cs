using System.Collections.Generic;
using UnityEngine;

public class Object_NpcQuest : MonoBehaviour, IActive, ISaveable
{
    [SerializeField] string idQuestTarget;
    [SerializeField] Sprite avtImage;
    [SerializeField] List<Quest> quests;


    public void Active()
    {
        UI_Controller.instance.EnableDialogueUI(true);
    }

    public void SaveData(ref GameData gameData)
    {
    }

    public void LoadData(GameData gameData)
    {
    }
}
