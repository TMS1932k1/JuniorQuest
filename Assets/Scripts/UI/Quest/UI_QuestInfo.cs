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
        if (GameManager.instance.isUpdateQuest)
        {
            GameManager.instance.isUpdateQuest = false;
            DisplayQuest();
        }
    }

    private void DisplayQuest()
    {
        if (GameManager.instance.GetCurrentQuest == null)
        {
            questString.text = "";
            return;
        }

        string questText = $"Quest: {GameManager.instance.GetCurrentQuest.name}";

        foreach (TargetGoal targetGoaled in GameManager.instance.GetTargetGoaleds)
        {
            TargetGoal targetGoal = GameManager.instance.GetTargetGoals.FirstOrDefault(tg => tg.idQuesTarget == targetGoaled.idQuesTarget);
            questText += $"\n- {targetGoaled.idQuesTarget}: {targetGoaled.count}/{targetGoal.count}";
        }

        questString.text = questText;
    }
}
