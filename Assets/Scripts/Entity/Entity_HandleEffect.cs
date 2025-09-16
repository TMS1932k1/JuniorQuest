using System.Collections;
using UnityEngine;

public class Entity_HandleEffect : MonoBehaviour, ICanBurn, ICanFreeze
{
    // Burn
    private bool canBurn;
    private Coroutine burnCoroutine;


    private Entity entity;
    private Entity_Health entityHealth;
    private Entity_VFX entityVFX;


    void Awake()
    {
        entity = GetComponent<Entity>();
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

    public void BeFreezed(float duration)
    {
        entity.BeFreezed(duration);
        entityVFX.PlayFreezedVFX(duration);

        Invoke(nameof(ExitFreezed), duration); // Exit freezed after duration
    }

    public void ExitFreezed() => entity.ExitFreezed();

    public void ResetHandleEffect()
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);
    }
}
