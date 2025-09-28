using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public void OnNewGameBTN()
    {
        Debug.Log("New Game");

        SaveManager.instance.DeleteData();
        GameManager.instance.LoadToScene();
    }

    public void OnLoadGameBTN()
    {
        Debug.Log("Load Game");

        GameManager.instance.LoadToScene();
    }

    public void OnSettingBTN()
    {
        Debug.Log("Setting Game");
    }
}
