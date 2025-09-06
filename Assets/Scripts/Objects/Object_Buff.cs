using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    [SerializeField] ObjectBuffDataSO data;


    [Header("Display")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveRange;
    [SerializeField] private Color defenceEffectColor = Color.yellow;
    [SerializeField] private Color damageEffectColor = Color.red;
    [SerializeField] private Color healthEffectColor = Color.green;


    // Components
    private SpriteRenderer sr;
    private ParticleSystem auraPs;
    private ParticleSystem buffPs;
    private Entity_Stat stat;


    private Coroutine buffCoroutine;
    private Vector2 originPosition;
    private Color effectColor;
    private bool isTaked;


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        auraPs = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        effectColor = GetColorOfBuff();
        originPosition = transform.position;
        ChangeColorPs(auraPs, effectColor);
    }

    private void Update()
    {
        moveVertical();
    }

    private void moveVertical()
    {
        float y = originPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector2(transform.position.x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTaked) return;

        if (buffCoroutine != null)
            StopCoroutine(buffCoroutine);

        buffCoroutine = StartCoroutine(BuffCo(collision));
    }

    private IEnumerator BuffCo(Collider2D col)
    {
        HideObject();

        ApplyBuff(true, col);
        yield return new WaitForSeconds(data.duration);
        ApplyBuff(false, col);
    }

    private void ApplyBuff(bool isApply, Collider2D col)
    {
        if (isApply)
        {
            Debug.Log("Apply buff");

            // Show TakeEffect
            ShowTakeEffect(col);

            // Modifier Stats
            stat = col.GetComponent<Entity_Stat>();
            if (stat)
                stat.AddModifierWithType(data.statType, data.source, data.value);
        }
        else
        {
            // Remove Modifier Stats
            if (stat)
                stat.RemoveModifierWithType(data.statType, data.source);

            Debug.Log("Overtime buff");
            Destroy(gameObject);
        }
    }

    private void ShowTakeEffect(Collider2D col)
    {
        buffPs = col.GetComponentInChildren<ParticleSystem>();
        if (buffPs)
        {
            ChangeColorPs(buffPs, effectColor);
            buffPs.Play();
        }
    }

    private void ChangeColorPs(ParticleSystem ps, Color color)
    {
        var main = ps.main;
        main.startColor = color;
    }

    private void HideObject()
    {
        isTaked = true;
        sr.color = Color.clear;
        auraPs.Stop();
    }

    private Color GetColorOfBuff()
    {
        switch (data.statType)
        {
            case EStat_Type.MaxHealth:
            case EStat_Type.Vitality:
                return healthEffectColor;

            case EStat_Type.Strength:
            case EStat_Type.CritChance:
            case EStat_Type.CritPower:
                return damageEffectColor;

            case EStat_Type.Agility:
            case EStat_Type.Armor:
                return defenceEffectColor;

            default:
                return Color.white;
        }
    }

    void OnValidate()
    {
        if (data == null)
            return;

        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = data.image;

        gameObject.name = "ObjectBuff_" + data.source.Replace(" ", "");
    }
}


