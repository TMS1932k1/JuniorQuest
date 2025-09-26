using UnityEngine;

public class Skill_FireBlade_Slash : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float speed;
    private float timer;
    private bool isHit;
    private float damage;


    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private ObjectPool_FireBlade pool;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponentInChildren<AudioSource>();

        pool = FindFirstObjectByType<ObjectPool_FireBlade>();
    }

    void OnEnable()
    {
        timer = float.MaxValue;
        isHit = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !isHit)
            Hide();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            isHit = true;

            rb.linearVelocity = Vector2.zero; // Stop moving

            anim.SetTrigger(PlayerAnimationStrings.HIT_TRIGGER);
            AudioManager.instance.PlayAudioClip(audioSource, ClipDataNameStrings.RANGE_ATTACK_HIT);

            if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.ENEMY_LAYER)) // Hit Enemy
            {
                collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, pool.transform);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.BREAKABLE_LAYER)) // Hit IBreakable
            {
                collision.gameObject.GetComponent<IBreakable>().Break();
            }
        }
    }

    public void SetMove()
    {
        rb.linearVelocity = transform.right * speed;
    }

    public void Hide()
    {
        pool.ReturnObject(this);
    }

    public void SetBladeDetails(float damage, float duration)
    {
        this.damage = damage;
        timer = duration;
        transform.position = pool.transform.position;
    }
}
