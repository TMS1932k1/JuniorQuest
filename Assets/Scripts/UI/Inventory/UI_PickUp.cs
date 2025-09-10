using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PickUp : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI stack;

    Color uiColor = Color.white;


    void Awake()
    {
        image = GetComponent<Image>();
        stack = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        SetEmpty();
    }

    public void SetDisplay(Slot slot)
    {
        ChangeEmptyColor(false);

        image.sprite = slot?.data.image;
        stack.text = slot?.stack.ToString();
    }

    public void SetEmpty()
    {
        ChangeEmptyColor(true);

        image.sprite = null;
        stack.text = "";
    }

    private void ChangeEmptyColor(bool isEmpty)
    {
        uiColor.a = isEmpty ? 0f : 1f;
        image.color = uiColor;
    }
}
