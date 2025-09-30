using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public void OnNewGameBTN()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_HOVER);

        SaveManager.instance.DeleteData();
        GameManager.instance.MainMenuToScene();
    }

    public void OnLoadGameBTN()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_HOVER);

        GameManager.instance.MainMenuToScene();
    }

    public void OnQuitBTN()
    {
        AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_HOVER);

        Application.Quit();
    }
}
