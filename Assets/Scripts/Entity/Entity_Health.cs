using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity entity;
    private Entity_Stat stat;

    [Header("Health")]
    [SerializeField] public float currentHealth;
    [Range(0, 1)]
    [SerializeField] public float heavyDamagePercent;

    public bool isDead;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        stat = GetComponent<Entity_Stat>();
    }

    protected virtual void Start()
    {
        currentHealth = stat.GetHealth();
        isDead = false;
    }

    public virtual void ReduceHealth(float damage, out bool isMissed, Transform damageDealer)
    {
        isMissed = MissAttack();

        if (isDead)
            return;
        if (isMissed)
            return;

        float finalDamage = CalculateReducedDamage(damage);

        currentHealth -= finalDamage;
        if (currentHealth <= 0)
            Die();

        if (!isDead)
            entity.ReceiveKnockBack(isHeavyAttack(finalDamage), CalculateKnockBackDir(damageDealer.position.x));
    }

    private float CalculateReducedDamage(float damage)
    {
        float reducePercent = stat.GetArmor() / (100 + stat.GetArmor()); // Scaling constant = 100
        return damage * (1 - Mathf.Clamp01(reducePercent));
    }

    private bool MissAttack()
    {
        return Random.Range(0, 100) <= stat.GetEvasion();
    }

    public float GetHealthPercent()
    {
        return currentHealth / stat.GetHealth();
    }

    private int CalculateKnockBackDir(float positionX)
    {
        return positionX > transform.position.x ? -1 : 1;
    }

    protected bool isHeavyAttack(float damage)
    {
        return damage / stat.GetHealth() > heavyDamagePercent;
    }

    protected virtual void Die()
    {
        isDead = true;
    }
}
