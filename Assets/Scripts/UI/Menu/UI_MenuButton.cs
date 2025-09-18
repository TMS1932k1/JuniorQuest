using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MenuButton : MonoBehaviour, IPointerClickHandler
{
    private UI_Menu menuUI;

    [SerializeField] private UI_Window windowUI;

    public bool isOpen { get; private set; } = false;
    private float sizeMutiplier = 1.3f;
    private Vector2 originScale;


    private void Awake()
    {
        menuUI = GetComponentInParent<UI_Menu>();
    }

    private void Start()
    {
        originScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOpen)
        {
            // Close orther icon
            menuUI.CloseOrtherIconOpen(this);

            // Open this icon
            isOpen = !isOpen;
            UpdateSizeIcon();

            // Handl click
            HandleClick();
        }
        else
        {
            CloseButton();
        }
    }

    private void UpdateSizeIcon()
    {
        if (isOpen)
            transform.localScale = originScale * sizeMutiplier;
        else
            transform.localScale = originScale;
    }

    protected virtual void HandleClick()
    {
        windowUI.ShowWindow();
    }

    public void CloseButton()
    {
        isOpen = false;
        UpdateSizeIcon();

        windowUI.HideWindow();
    }
}
