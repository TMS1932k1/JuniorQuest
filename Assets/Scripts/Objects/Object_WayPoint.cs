using TMPro;
using UnityEngine;

public class Object_WayPoint : MonoBehaviour
{
    [SerializeField] SceneSO sceneData;
    [SerializeField] EWayPoint_Type type;
    [SerializeField] EWayPoint_Type connectType;
    [SerializeField] Transform teleportPoint;


    public EWayPoint_Type GetWayPointType() => type;
    public EWayPoint_Type GetConnectType() => connectType;
    public Vector3 GetTeleportPoint() => teleportPoint.position;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (sceneData == null)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER))
        {
            GameManager.instance.ChangeToScene(sceneData.sceneName, connectType);
        }
    }

    private void OnValidate()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();

        if (type == EWayPoint_Type.Enter)
        {
            text.text = "Next";
            connectType = EWayPoint_Type.Exit;
        }

        if (type == EWayPoint_Type.Exit)
        {
            text.text = "Back";
            connectType = EWayPoint_Type.Enter;
        }

        if (sceneData != null)
            gameObject.name = "WayPoint_" + type.ToString() + "_" + sceneData.sceneName;
    }
}
