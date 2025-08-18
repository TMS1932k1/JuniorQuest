using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    [Header("On Damge VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageDuration;

    private Material originMaterial;
    private SpriteRenderer sr;

    private Coroutine onDamageVFXCoroutine;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMaterial = sr.material;
    }

    public void PlayOnDamageVFXCO()
    {
        if (onDamageVFXCoroutine != null)
            StopCoroutine(onDamageVFXCoroutine);

        onDamageVFXCoroutine = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo()
    {
        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageDuration);
        sr.material = originMaterial;
    }
}
