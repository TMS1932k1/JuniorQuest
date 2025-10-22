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

    private Material originMaterial;
    private Coroutine onDamageVFXCoroutine;


    [Header("Effects VFX")]
    [SerializeField] Color burnColor = Color.red;
    [SerializeField] Color burnDarkColor = Color.red;
    [SerializeField] Color freezeColor = Color.blue;

    private Color originalColor;
    private Coroutine burnCoroutine;
    private Coroutine freezedCoroutine;


    //Component
    protected SpriteRenderer sr;


    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        originalColor = sr.color;
    }

    protected virtual void Start()
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

    public void PlayOnDamageVFXCo()
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

    public void PlayBurnVFXCo(float interval)
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        burnCoroutine = StartCoroutine(BurnCo(interval));
    }

    private IEnumerator BurnCo(float interval)
    {
        while (true)
        {
            sr.color = sr.color == burnColor ? burnDarkColor : burnColor;

            yield return new WaitForSeconds(interval);
        }
    }

    public void StopBurnVFXCo()
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        sr.color = originalColor;
    }

    public void PlayFreezedVFX(float duration)
    {
        if (freezedCoroutine != null)
            StopCoroutine(freezedCoroutine);

        freezedCoroutine = StartCoroutine(FreezedCo(duration));
    }

    private IEnumerator FreezedCo(float duration)
    {
        sr.color = freezeColor;
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;
    }

    public virtual void ResetVFX()
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        if (freezedCoroutine != null)
            StopCoroutine(freezedCoroutine);

        if (onDamageVFXCoroutine != null)

            StopCoroutine(onDamageVFXCoroutine);

        sr.material = originMaterial;
        sr.color = originalColor;
    }
}
