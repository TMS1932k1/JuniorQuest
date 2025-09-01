using UnityEngine;

public class Skill_FireBlade_Slash : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float speed;
    [SerializeField] Transform playerTransform;


    private Animator anim;

    private float timer;
    private bool isHit;
    private float damage;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        timer = float.MaxValue;
        isHit = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (!isHit)
            MoveSlash();

        if (timer <= 0 && !isHit)
            HideSlash();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("hit");
        isHit = true;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Entity_Health>().ReduceHealth(damage, out bool isMissed, playerTransform);
        }
    }

    private void MoveSlash()
    {
        // Slash move straight
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void HideSlash()
    {
        transform.position = playerTransform.position;
        transform.rotation = Quaternion.identity;

        gameObject.SetActive(false);
    }

    public void SetBladeDetails(float damage, float duration)
    {
        this.damage = damage;
        timer = duration;
    }
}
