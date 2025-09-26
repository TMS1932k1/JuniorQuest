using UnityEngine;

public class Gollux_Rock : MonoBehaviour
{
    // Components
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private ObjectPool<Gollux_Rock> pool;


    private bool isHit;
    private float originGravity;
    private float damage;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponentInChildren<AudioSource>();

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
            rb.linearVelocityY = 0f;

            anim.SetTrigger(EnemyAnimationStrings.HIT_TRIGGER);
            AudioManager.instance.PlayAudioClip(audioSource, ClipDataNameStrings.RANGE_ATTACK_HIT);

            if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER))
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
