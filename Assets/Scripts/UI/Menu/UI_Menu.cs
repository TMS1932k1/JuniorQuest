using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private UI_MenuButton[] buttons;

    public void CloseOrtherIconOpen(UI_MenuButton clickButton)
    {
        foreach (UI_MenuButton button in buttons)
        {
            if (button.isOpen)
                button.CloseButton();
        }
    }
}
