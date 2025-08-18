using System;
using System.Collections;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity entity;

    [Header("Health")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [Range(0, 1)]
    [SerializeField] public float heavyDamagePercent;

    public bool isDead;

    void Start()
    {
        entity = GetComponent<Entity>();

        currentHealth = maxHealth;
        isDead = false;
    }

    public virtual void ReduceHealth(float damage, Transform damageDealer)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        if (!isDead)
            entity.ReceiveKnockBack(isHeavyAttack(damage), CalculateKnockBackDir(damageDealer.position.x));
    }

    private int CalculateKnockBackDir(float positionX)
    {
        return positionX > transform.position.x ? -1 : 1;
    }

    protected bool isHeavyAttack(float damage)
    {
        return damage / maxHealth > heavyDamagePercent;
    }

    protected virtual void Die()
    {
        isDead = true;
    }
}
