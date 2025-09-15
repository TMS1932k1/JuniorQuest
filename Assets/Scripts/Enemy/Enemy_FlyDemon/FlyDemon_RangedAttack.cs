using UnityEngine;

public class FlyDemon_RangedAttack : MonoBehaviour, ICanCounter
{
    [Header("Details")]
    [SerializeField] float speed;
    [SerializeField] float burnDuration;
    [SerializeField] int hitCount;
    [Range(0, 1)]
    [SerializeField] float burnDamageMutiplier;

    private float damage;
    private bool isHit;
    private bool canCounter;


    private Animator anim;
    private Rigidbody2D rb;
    private ObjectPool<FlyDemon_RangedAttack> pool;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pool = GetComponentInParent<ObjectPool<FlyDemon_RangedAttack>>();
    }

    void OnEnable()
    {
        ResetState();
    }

    public void HandleCounter()
    {
        canCounter = false;

        // Change move direction
        transform.localScale = new Vector3(-1, 1, 1);
        rb.linearVelocity = transform.right * -speed;

        // Change layer
        gameObject.layer = LayerMask.NameToLayer(ELayer.PlayerAttack.ToString());
    }

    public bool GetCanCounter => canCounter;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            isHit = true;
            rb.linearVelocity = Vector2.zero; // Stop moving
            anim.SetTrigger(EParamenter_Enemy.hit.ToString());

            if (collision.gameObject.layer == LayerMask.NameToLayer(!canCounter ? ELayer.Enemy.ToString() : ELayer.Player.ToString()))
            {
                collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, transform);

                if (!isMissed)
                {
                    ICanBurn canBurnTarget = collision.GetComponent<ICanBurn>();
                    if (canBurnTarget != null && canBurnTarget.GetCanBurn())
                    {
                        float duration = burnDuration;
                        int countHit = hitCount;

                        canBurnTarget.BeBurn(damage * burnDamageMutiplier, duration, countHit);
                    }
                }
            }
        }
    }

    public void SetDetails(Vector3 position, float angleZ, float damage)
    {
        this.damage = damage;

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }

    public void Hide()
    {
        pool.ReturnObject(this);
    }

    public void SetMove()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void ResetState()
    {
        isHit = false;
        canCounter = true;
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.layer = LayerMask.NameToLayer(ELayer.EnemyAttack.ToString());
    }
}
