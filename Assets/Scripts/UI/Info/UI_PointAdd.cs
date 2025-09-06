using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PointAdd : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UI_PointManager pointManager;
    [SerializeField] EStat_Type type;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pointManager.CanIncrementStat())
        {
            pointManager.IncrementStatWithType(type);
        }
        else
        {
            Debug.Log("Not enought point to add");
        }
    }
}
