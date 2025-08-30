using System.Collections;
using UnityEngine;

public class Object_Interatable : MonoBehaviour
{
    [Header("Move deteils")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveRange;


    [Header("Buff details")]
    [SerializeField] private float buffDuration = 1f;
    [SerializeField] private Color defenceEffectColor = Color.yellow;
    [SerializeField] private Color damageEffectColor = Color.red;
    [SerializeField] private Color healthEffectColor = Color.green;
    [SerializeField] private StatType statType;
    [SerializeField] private string buffSource;
    [SerializeField] private float buffValue;


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
        yield return new WaitForSeconds(buffDuration);
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
                stat.GetStatWithType(statType).AddModifier(buffSource, buffValue);
        }
        else
        {
            // Remove Modifier Stats
            if (stat)
                stat.GetStatWithType(statType).RemoveModifier(buffSource);

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
        switch (statType)
        {
            case StatType.MaxHealth:
            case StatType.Vitality:
                return healthEffectColor;

            case StatType.Strength:
            case StatType.CritChance:
            case StatType.CritPower:
                return damageEffectColor;

            case StatType.Agility:
            case StatType.Armor:
                return defenceEffectColor;

            default:
                return Color.white;
        }
    }
}


