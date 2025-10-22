using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_QuestInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questString;


    private void OnEnable()
    {
        DisplayQuest();
    }

    private void Update()
    {
        if (GameManager.instance.questUpdateStatus != QuestUpdateStatus.None)
        {
            DisplayQuest();
            GameManager.instance.questUpdateStatus = QuestUpdateStatus.None;
        }
    }

    private void DisplayQuest()
    {
        if (GameManager.instance.GetCurrentQuest == null)
        {
            questString.text = "";
            return;
        }

        // Set text quest info
        string questText = $"Quest: {GameManager.instance.GetCurrentQuest.questName}";
        if (GameManager.instance.questUpdateStatus == QuestUpdateStatus.Complete)
            questText += " [COMPLETED]";

        foreach (TargetGoal targetGoaled in GameManager.instance.GetTargetGoaleds)
        {
            TargetGoal targetGoal = GameManager.instance.GetTargetGoals.FirstOrDefault(tg => tg.idQuesTarget == targetGoaled.idQuesTarget);
            questText += $"\n- {targetGoaled.idQuesTarget}: {targetGoaled.count}/{targetGoal.count}";
        }
        questString.text = questText;

        // When quest is done
        if (GameManager.instance.questUpdateStatus == QuestUpdateStatus.Complete)
            HandleCompleteQuest();
    }

    private void HandleCompleteQuest()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_COMPLETE);
        questString.text = "";
        GameManager.instance.RemoveQuest();
    }
}
