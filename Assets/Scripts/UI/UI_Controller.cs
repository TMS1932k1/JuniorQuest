using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;


    [SerializeField] UI_Dialogue dialogueUI;
    [SerializeField] Canvas inGameUI;


    private void Awake()
    {
        instance = this;
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
