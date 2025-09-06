using UnityEngine;

public class Object_Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(ELayer.Player.ToString()))
            return;

        Player_Health playerHealth = other.gameObject.GetComponent<Player_Health>();
        playerHealth.Die();
    }
}
