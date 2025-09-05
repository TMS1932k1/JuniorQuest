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
    private ObjectPool_FireBlade pool;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pool = GetComponentInParent<ObjectPool_FireBlade>();
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
            HideSlash();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        rb.linearVelocity = Vector2.zero; // Stop moving
        anim.SetTrigger("hit");
        isHit = true;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, pool.transform);
        }
    }

    public void SetMove()
    {
        rb.linearVelocity = transform.right * speed;
    }

    public void HideSlash()
    {
        transform.rotation = Quaternion.identity; // Reset rotate
        pool.ReturnObject(this);
    }

    public void SetBladeDetails(float damage, float duration)
    {
        this.damage = damage;
        timer = duration;
    }
}
