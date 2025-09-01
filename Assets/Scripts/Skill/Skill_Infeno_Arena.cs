using UnityEngine;

public class Skill_Infeno_Arena : MonoBehaviour
{
    [Header("Display")]
    private float width;
    private float height;
    private Vector2 position;


    [Header("Details")]
    private float damage;
    private float effectDuration;
    private int countHit;
    private LayerMask whatIsBurn;


    void Update()
    {
        // Keep position, not follow player
        transform.position = position;

        Burn();
    }

    private void Burn()
    {
        Collider2D[] burnTargets = Physics2D.OverlapBoxAll(transform.position, new Vector2(width, height), 0, whatIsBurn);

        foreach (Collider2D target in burnTargets)
        {
            ICanBurn canBurnTarget = target.GetComponent<ICanBurn>();

            if (canBurnTarget != null && canBurnTarget.GetCanBurn())
            {
                canBurnTarget.BeBurn(damage, effectDuration, countHit);
            }
        }
    }

    public void SetArenaDetails(SkillDataSO data, float damage, Vector2 position, LayerMask whatIsBurn)
    {
        height = data.heightArena;
        width = data.widthArena;
        effectDuration = data.effectDuration;
        countHit = data.countHit;

        this.damage = damage;
        this.position = position;
        this.whatIsBurn = whatIsBurn;
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
