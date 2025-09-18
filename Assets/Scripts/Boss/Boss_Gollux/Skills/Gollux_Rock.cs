using UnityEngine;

public class Gollux_Rock : MonoBehaviour
{
    // Components
    private Animator anim;
    private Rigidbody2D rb;
    private ObjectPool<Gollux_Rock> pool;


    private bool isHit;
    private float originGravity;
    private float damage;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pool = GetComponentInParent<ObjectPool<Gollux_Rock>>();

        originGravity = rb.gravityScale;
    }

    private void OnEnable()
    {
        ResetState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            isHit = true;

            // Stop drop
            rb.gravityScale = 0f;
            rb.linearVelocityY = 0;

            anim.SetTrigger(EnemyAnimationStrings.hitTrigger);

            if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PlayerLayer))
            {
                collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, transform);
            }
        }
    }

    public void Drop()
    {
        rb.gravityScale = originGravity;
    }

    public void SetDetails(Vector3 position, float damage)
    {
        this.damage = damage;
        transform.position = position;
    }

    public void Hide()
    {
        pool.ReturnObject(this);
    }

    private void ResetState()
    {
        rb.gravityScale = 0f;
        isHit = false;
    }
}
