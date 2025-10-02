using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialogue : UI_Window
{
    [Header("Dialogue Details")]
    [SerializeField] Image avtImage;
    [SerializeField] TextMeshProUGUI questDecription;


    public void OnNextDialogue()
    {
        UI_Controller.instance.EnableDialogueUI(false);
    }
}
