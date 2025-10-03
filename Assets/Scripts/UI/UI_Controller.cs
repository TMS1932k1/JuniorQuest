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
        controlUI.gameObject.SetActive(true);

        if (Application.isMobilePlatform)
            Debug.Log("Game is playing on mobile device");

        else
            Debug.Log("Game is not playing on mobile device");
    }

    public void EnableDialogueUI(bool enable)
    {
        inGameUI.gameObject.SetActive(!enable);

        if (enable)
            dialogueUI.ShowWindow();
        else
            dialogueUI.HideWindow();
    }
}
