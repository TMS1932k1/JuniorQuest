using UnityEngine;

public class Object_WayPoint : MonoBehaviour
{
    [SerializeField] EWayPoint_Type type;
    [SerializeField] EWayPoint_Type connecType;


    private bool canTeleport = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER) && canTeleport)
        {
            canTeleport = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER) && !canTeleport)
        {
            canTeleport = true;
        }
    }
}
