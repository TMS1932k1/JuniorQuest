using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PointReset : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UI_PointManager pointManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        pointManager.ResetPoint();
    }
}
