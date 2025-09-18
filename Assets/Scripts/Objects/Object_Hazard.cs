using UnityEngine;

public class Object_Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER)
            && collision.gameObject.layer != LayerMask.NameToLayer(LayerStrings.ENEMY_LAYER)
            && collision.gameObject.layer != LayerMask.NameToLayer(LayerStrings.INVISIBILITY_LAYER))
            return;

        Entity_Health entityHealth = collision.gameObject.GetComponent<Entity_Health>();
        entityHealth.Die();
    }
}
