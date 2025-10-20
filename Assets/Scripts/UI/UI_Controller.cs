using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;


    [SerializeField] Canvas dialogueUI;
    [SerializeField] Canvas inGameUI;
    [SerializeField] Canvas inputlUI;


    private QuestSO questData;


    private void Awake()
    {
        instance = this;
        inputlUI.gameObject.SetActive(Application.isMobilePlatform);
        EnableUI(dialogueUI, false);

        if (Application.isMobilePlatform)
            Debug.Log("Game is playing on mobile device");
        else
            Debug.Log("Game is not playing on mobile device");
    }

    public void ShowDialogueUI(string[] dialoges, Sprite avtImage)
    {
        UI_Dialogue dialogue = dialogueUI.gameObject.GetComponent<UI_Dialogue>();
        dialogue.SetDialogueUI(avtImage, dialoges);

        EnableUI(dialogueUI, true);
        EnableUI(inGameUI, false);
        EnableUI(inputlUI, false);

        Time.timeScale = 0f;
    }

    public void ShowDialogueUI(QuestSO questData, Sprite avtImage)
    {
        this.questData = questData;

        UI_Dialogue dialogue = dialogueUI.gameObject.GetComponent<UI_Dialogue>();
        dialogue.SetDialogueUI(avtImage, questData.dialoges);

        EnableUI(dialogueUI, true);
        EnableUI(inGameUI, false);
        EnableUI(inputlUI, false);

        Time.timeScale = 0f;
    }

    public void HideDialogueUI()
    {
        Time.timeScale = 1f;

        EnableUI(dialogueUI, false);
        EnableUI(inGameUI, true);
        EnableUI(inputlUI, Application.isMobilePlatform);

        if (questData != null)
        {
            Debug.Log("Update Test");
            GameManager.instance.TakeQuest(questData);
            questData = null;
        }

    }

    public void EnableInputUI(bool enable)
    {
        inputlUI.GetComponent<CanvasGroup>().interactable = enable;
        inputlUI.GetComponent<CanvasGroup>().blocksRaycasts = enable;
    }

    private void EnableUI(Canvas ui, bool enable)
    {
        CanvasGroup group = ui.GetComponent<CanvasGroup>();

        if (group == null)
            return;

        group.alpha = enable ? 1 : 0;
        group.interactable = enable;
        group.blocksRaycasts = enable;
    }
}
