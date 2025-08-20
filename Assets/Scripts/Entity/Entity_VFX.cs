using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    [Header("On Damge VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageDuration;


    [Header("Hit VFX")]
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject critHitVFX;
    [SerializeField] Color hitVFXColor;
    [SerializeField] float timeDestroy = 1f;
    [SerializeField] float minRandomX;
    [SerializeField] float maxRandomX;
    [SerializeField] float minRandomY;
    [SerializeField] float maxRandomY;


    //Component
    private SpriteRenderer sr;


    private Material originMaterial;
    private Coroutine onDamageVFXCoroutine;


    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        originMaterial = sr.material;
    }

    public void CreateHitVFX(Vector3 position, bool isCrit)
    {
        hitVFX.GetComponentInChildren<SpriteRenderer>().color = hitVFXColor;
        critHitVFX.GetComponentInChildren<SpriteRenderer>().color = hitVFXColor;

        GameObject hit = Instantiate(isCrit ? critHitVFX : hitVFX, RandomInstantiatePosition(position), RandomRotate());
        Destroy(hit, timeDestroy);
    }

    public Vector3 RandomInstantiatePosition(Vector3 position)
    {
        float x = Random.Range(minRandomX, maxRandomX);
        float y = Random.Range(minRandomY, maxRandomY);

        return position + new Vector3(x, y);
    }

    public Quaternion RandomRotate() => Quaternion.Euler(0, 0, Random.Range(0, 360));

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
