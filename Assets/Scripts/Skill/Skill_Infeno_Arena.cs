using UnityEngine;

public class Skill_Infeno_Arena : MonoBehaviour
{
    private Skill_Infeno infeno;

    private float height;
    private float width;
    private float damage;


    void Update()
    {
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
                int countHit = infeno.skillData.hitCount;

                canBurnTarget.BeBurn(damage, duration, countHit);
            }
        }
    }

    public void SetArenaDetails(float damage, Vector2 position, Skill_Infeno infeno)
    {
        this.infeno = infeno;
        this.damage = damage;

        transform.position = position;

        width = infeno.skillData.widthArena;
        height = infeno.skillData.heightArena;
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
