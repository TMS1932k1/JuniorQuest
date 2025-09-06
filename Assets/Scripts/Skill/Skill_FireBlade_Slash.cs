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
            HideSlash();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            isHit = true;
            rb.linearVelocity = Vector2.zero; // Stop moving
            anim.SetTrigger(EParamenter_Player.hit.ToString());

            if (collision.gameObject.layer == LayerMask.NameToLayer(ELayer.Enemy.ToString()))
            {
                collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, pool.transform);
            }
        }
    }

    public void SetMove()
    {
        rb.linearVelocity = transform.right * speed;
    }

    public void HideSlash()
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
