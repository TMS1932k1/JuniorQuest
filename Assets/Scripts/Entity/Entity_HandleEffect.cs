using System.Collections;
using UnityEngine;

public class Entity_HandleEffect : MonoBehaviour, ICanBurn
{
    // Burn
    private bool canBurn;
    private Coroutine burnCoroutine;


    private Entity_Health entityHealth;
    private Entity_VFX entityVFX;


    void Awake()
    {
        entityHealth = GetComponent<Entity_Health>();
        entityVFX = GetComponent<Entity_VFX>();
    }

    void Start()
    {
        canBurn = true;
    }

    public bool GetCanBurn() => canBurn;

    public void BeBurn(float damage, float duration, int countHit)
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        burnCoroutine = StartCoroutine(BurnCo(damage, duration, duration / countHit));
    }

    private IEnumerator BurnCo(float damage, float durationTimer, float hitInterval)
    {
        canBurn = false;
        entityVFX.PlayBurnVFXCo(hitInterval); // Show VFX

        // Damage
        while (durationTimer > 0)
        {
            entityHealth.ReduceHealthNotMiss(damage, null);

            yield return new WaitForSeconds(hitInterval);
            durationTimer -= hitInterval;
        }

        canBurn = true;
        entityVFX.StopBurnVFXCo(); // Off VFX
    }
}
