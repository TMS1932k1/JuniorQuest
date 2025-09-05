using UnityEngine;

public class Skill_Infeno_Arena : MonoBehaviour
{
    private Skill_Infeno infeno;

    private Vector2 position;
    private float height;
    private float width;
    private float damage;


    void Awake()
    {
        infeno = GetComponentInParent<Skill_Infeno>();
        width = infeno.skillData.widthArena;
        height = infeno.skillData.heightArena;
    }

    void Update()
    {
        // Keep position, not follow player
        transform.position = position;

        Burn();
    }

    private void Burn()
    {
        // Get all targets in arena
        Collider2D[] burnTargets = Physics2D.OverlapBoxAll(transform.position, new Vector2(width, height), 0, infeno.whatIsBurn);

        // Burn effect to targets
        foreach (Collider2D target in burnTargets)
        {
            ICanBurn canBurnTarget = target.GetComponent<ICanBurn>();
            if (canBurnTarget != null && canBurnTarget.GetCanBurn())
            {
                float duration = infeno.skillData.effectDuration;
                int countHit = infeno.skillData.countHit;

                canBurnTarget.BeBurn(damage, duration, countHit);
            }
        }
    }

    public void SetArenaDetails(float damage, Vector2 position)
    {
        this.damage = damage;
        this.position = position;
    }

    private void OnDrawGizmos()
    {
        if (transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector2(width, height));
        }
    }
}
