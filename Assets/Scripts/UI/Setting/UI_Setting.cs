using UnityEngine;

public class UI_Setting : UI_Window
{
    public void ChangeToMainMenu()
    {
        HideWindow();
        GameManager.instance.SceneToMainMenu();
    }
}
