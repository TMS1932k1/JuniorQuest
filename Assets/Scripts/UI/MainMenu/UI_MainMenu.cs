using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public void OnNewGameBTN()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_HOVER);

        SaveManager.instance.DeleteData();
        GameManager.instance.LoadToScene();
    }

    public void OnLoadGameBTN()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_HOVER);

        GameManager.instance.LoadToScene();
    }

    public void OnSettingBTN()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_HOVER);

        Debug.Log("Setting Game");
    }
}
