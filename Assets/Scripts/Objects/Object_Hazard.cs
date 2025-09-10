using UnityEngine;

public class Object_Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(ELayer.Player.ToString())
            && collision.gameObject.layer != LayerMask.NameToLayer(ELayer.Enemy.ToString())
            && collision.gameObject.layer != LayerMask.NameToLayer(ELayer.Invisibility.ToString()))
            return;

        Entity_Health entityHealth = collision.gameObject.GetComponent<Entity_Health>();
        entityHealth.Die();
    }
}
