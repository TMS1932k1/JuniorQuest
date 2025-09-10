using UnityEngine;

public class Object_HangingPlatform : MonoBehaviour
{
    [SerializeField] float gravity;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DropPlatform()
    {
        rb.gravityScale = gravity;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}
