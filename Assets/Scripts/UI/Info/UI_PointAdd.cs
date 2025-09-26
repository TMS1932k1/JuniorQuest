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
            AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_CONFIRM);

            pointManager.IncrementStatWithType(type);
        }
        else
        {
            AudioManager.instance.PlayUIAudioClip(ClipDataNameStrings.UI_DENIED);
            Debug.Log("Not enought point to add");
        }
    }
}
