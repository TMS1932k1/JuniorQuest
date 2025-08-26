using System.Collections;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity entity;
    private Entity_Stat stat;

    [Header("Health")]
    public float currentHealth;
    [Range(0, 1)]
    public float heavyDamagePercent;


    [Header("Auto Restore Health")]
    public float timeAutoRestoreHp;
    public float restorePerSecond;
    private Coroutine restoreHpCoroutine;


    public bool isDead;
    private float lastTimeTakeDamage;

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

    protected virtual void Update()
    {
        AutoRestoreHealth();
    }

    public virtual void ReduceHealth(float damage, out bool isMissed, Transform damageDealer)
    {
        // Cancle take damage
        isMissed = MissAttack();
        if (isDead)
            return;
        if (isMissed)
            return;

        // Stop restore HP
        StopAutoRestoreHP();

        // Reduce damage
        float finalDamage = CalculateReducedDamage(damage);
        currentHealth -= finalDamage;

        // Change state
        if (currentHealth <= 0)
            Die();

        // Knock back
        if (!isDead)
            entity.ReceiveKnockBack(isHeavyAttack(finalDamage), CalculateKnockBackDir(damageDealer.position.x));
    }

    /// <summary>
    /// When player not take damage during time (timeAutoRestoreHp)
    /// Then player auto restore health per 1 second (restorePerSecond)
    /// </summary>
    private void AutoRestoreHealth()
    {
        // Don't take damage 
        // Isn't in retsore HP status 
        // CurrentHp isn't full
        if (Time.time > lastTimeTakeDamage + timeAutoRestoreHp
            && restoreHpCoroutine == null
            && currentHealth < stat.GetHealth())
        {
            // Perform restore HP
            restoreHpCoroutine = StartCoroutine(AutoRestoreHealthCo());
        }
    }

    private IEnumerator AutoRestoreHealthCo()
    {
        while (currentHealth < stat.GetHealth())
        {
            currentHealth = Mathf.Min(currentHealth + restorePerSecond, stat.GetHealth()); // Don't over max health
            yield return new WaitForSeconds(1f);
        }
    }

    private void StopAutoRestoreHP()
    {
        lastTimeTakeDamage = Time.time;
        if (restoreHpCoroutine != null)
        {
            StopCoroutine(restoreHpCoroutine);
            restoreHpCoroutine = null;
        }
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
