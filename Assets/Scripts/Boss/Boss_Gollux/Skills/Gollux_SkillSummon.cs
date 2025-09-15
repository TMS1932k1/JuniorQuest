using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gollux_SkillSummon : MonoBehaviour
{

    [SerializeField] float healTimer;
    [SerializeField] int enemyCount = 1;

    public bool canSummon { get; private set; }
    private float summonHeight = 3;
    private float summonDistanceMutiplier = 3f;
    private float healValue;


    [SerializeField] ObjectPool<GolluxSummon> objectPool;
    private List<GolluxSummon> summons = new();


    private Boss_Health health;


    private void Awake()
    {
        health = GetComponentInParent<Boss_Health>();
    }

    private void Start()
    {
        canSummon = true;
    }

    public float GetHealTimer() => healTimer;

    public void Perform()
    {
        canSummon = false;

        for (int i = 0; i < enemyCount; i++)
        {
            GolluxSummon summon = objectPool.GetObject();
            summons.Add(summon);

            summon.transform.position = new Vector2(
                transform.position.x + summonDistanceMutiplier * i,
                transform.position.y + summonHeight
            );
        }
    }

    /// <summary>
    /// Dismmiss all summoned enemies
    /// Calculate total HP of all
    ///  - if healValue == 0 is all summon enemies were death => can't heal
    /// </summary>
    /// <returns>Heal value</returns>
    public void DismissAllSummon()
    {
        canSummon = true;
        healValue = 0;

        // Dismiss all enemies
        foreach (GolluxSummon summon in summons)
        {
            if (summon.isDismiss)
                continue;

            summon.DismissSummon(out float currentHealth);
            healValue += currentHealth;
        }
    }

    public void Heal()
    {
        Debug.Log("Heal");
        health.Heal(healValue);
    }

    public bool CanHeal()
    {
        bool canHeal = false;

        foreach (GolluxSummon summon in summons)
        {
            if (!summon.isDismiss)
            {
                canHeal = true;
                break;
            }
        }

        return canHeal;
    }
}
