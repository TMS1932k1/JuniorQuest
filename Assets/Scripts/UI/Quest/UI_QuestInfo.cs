using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_QuestInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questString;

    private Coroutine vfxCompleteCouroutine;


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
        if (vfxCompleteCouroutine != null)
            StopCoroutine(vfxCompleteCouroutine);
        vfxCompleteCouroutine = StartCoroutine(VfxCompleteCo());
    }

    private IEnumerator VfxCompleteCo()
    {
        questString.color = Color.green;
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_COMPLETE);
        yield return new WaitForSeconds(1f);

        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t); // Scale
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Restart UI
        questString.text = "";
        questString.color = Color.white;
        gameObject.transform.localScale = Vector3.one;

        GameManager.instance.RemoveQuest();
    }
}
