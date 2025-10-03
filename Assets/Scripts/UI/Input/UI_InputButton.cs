using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InputButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    private Animator anim;

    private bool isPressed;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isPressed)
        {
            isPressed = true;
            anim.SetTrigger(UIAnimationStrings.PRESS_TRIGGER);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPressed)
        {
            isPressed = false;
            anim.SetTrigger(UIAnimationStrings.EXIT_TRIGGER);
        }
    }
}
