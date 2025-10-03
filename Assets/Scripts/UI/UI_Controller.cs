using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;


    [SerializeField] UI_Dialogue dialogueUI;
    [SerializeField] Canvas inGameUI;
    [SerializeField] Canvas inputlUI;


    private void Awake()
    {
        instance = this;
        inputlUI.gameObject.SetActive(Application.isMobilePlatform);

        if (Application.isMobilePlatform)
            Debug.Log("Game is playing on mobile device");
        else
            Debug.Log("Game is not playing on mobile device");
    }

    public void EnableDialogueUI(bool enable)
    {
        // Set other UI
        EnableUI(inGameUI, !enable);
        EnableUI(inputlUI, !enable && Application.isMobilePlatform);

        if (enable)
            dialogueUI.ShowWindow();
        else
            dialogueUI.HideWindow();
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
