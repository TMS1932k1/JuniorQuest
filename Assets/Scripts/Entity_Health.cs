using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    public bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public virtual void ReduceHealth(float damage, Transform damageTransform)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Die");
    }
}
