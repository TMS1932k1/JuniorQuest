using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;


    [SerializeField] UI_Dialogue dialogueUI;
    [SerializeField] Canvas inGameUI;
    [SerializeField] Canvas controlUI;


    private void Awake()
    {
        instance = this;
        controlUI.gameObject.SetActive(Application.isMobilePlatform);

        if (Application.isMobilePlatform)
            Debug.Log("Game is playing on mobile device");
        else
            Debug.Log("Game is not playing on mobile device");
    }

    public void EnableDialogueUI(bool enable)
    {
        // Set other UI
        inGameUI.gameObject.SetActive(!enable);
        controlUI.gameObject.SetActive(!enable && Application.isMobilePlatform);

        if (enable)
            dialogueUI.ShowWindow();
        else
            dialogueUI.HideWindow();
    }
}
