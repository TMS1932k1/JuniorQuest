using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    private UI_MenuButton[] buttons;


    private void Awake()
    {
        buttons = FindObjectsByType<UI_MenuButton>(FindObjectsSortMode.None);
    }

    public void CloseOrtherIconOpen(UI_MenuButton clickButton)
    {
        foreach (UI_MenuButton button in buttons)
        {
            if (button.isOpen)
                button.HandleClose();
        }
    }
}
