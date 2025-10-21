using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialogue : UI_Window
{
    [Header("Dialogue Details")]
    [SerializeField] Image avtImage;
    [SerializeField] TextMeshProUGUI questDialogue;
    [SerializeField] TextMeshProUGUI acceptButton;

    private string[] dialogues;
    private int i;


    public void SetDialogueUI(Sprite avt, string[] dialogues)
    {
        this.dialogues = dialogues;
        avtImage.sprite = avt;

        HandleDialogue();
    }

    /// <summary>
    /// Handle next dialogue text 
    /// and hide UI when finish
    /// </summary>
    public void HandleDialogue()
    {
        if (i > 0)
            AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_DECIDE);

        if (i < dialogues.Length)
        {
            if (i >= dialogues.Length - 1)
                acceptButton.text = "Accept";
            questDialogue.text = dialogues[i++];
        }
        else
        {
            i = 0;
            acceptButton.text = "Next";

            UI_Controller.instance.HideDialogueUI();
        }
    }
}
