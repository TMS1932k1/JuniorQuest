using UnityEngine;

public class FlyDemon_RangedAttack : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float speedMove;
    private float damage;
    private bool isCrit;


    private Rigidbody2D rb;
    private ObjectPool<FlyDemon_RangedAttack> pool;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pool = GetComponentInParent<ObjectPool<FlyDemon_RangedAttack>>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, transform);
        }

        pool.ReturnObject(this);
    }

    public void SetDetails(Vector2 position, float angleZ, float damage, bool isCrit)
    {
        this.damage = damage;
        this.isCrit = isCrit;

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }

    public void SetMove()
    {
        rb.linearVelocity = transform.right * speedMove;
    }
}
