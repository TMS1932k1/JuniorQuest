using UnityEngine;

public class Object_Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(LayerStrings.PlayerLayer)
            && collision.gameObject.layer != LayerMask.NameToLayer(LayerStrings.EnemyLayer)
            && collision.gameObject.layer != LayerMask.NameToLayer(LayerStrings.InvisibilityLayer))
            return;

        Entity_Health entityHealth = collision.gameObject.GetComponent<Entity_Health>();
        entityHealth.Die();
    }
}
