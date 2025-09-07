using System.Collections;
using UnityEngine;

public class Object_Buff : Object_Interactable
{
    [Header("Data")]
    [SerializeField] ObjectBuffDataSO data;
    private Entity_Stat stat;


    private Color defenceEffectColor = Color.yellow;
    private Color damageEffectColor = Color.red;
    private Color healthEffectColor = Color.green;


    private ParticleSystem buffPs;
    private Coroutine buffCoroutine;


    protected override void Start()
    {
        base.Start();

        effectColor = GetColorOfBuff();
        ChangeColorPs(auraPs, effectColor);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTaked) return;

        if (buffCoroutine != null)
            StopCoroutine(buffCoroutine);
        buffCoroutine = StartCoroutine(BuffCo(collision));

        base.OnTriggerEnter2D(collision);
    }

    private IEnumerator BuffCo(Collider2D col)
    {
        ApplyBuff(true, col);
        yield return new WaitForSeconds(data.duration);
        ApplyBuff(false, col);
    }

    private void ApplyBuff(bool isApply, Collider2D col)
    {
        if (isApply)
        {
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

        gameObject.name = "Buff_" + data.source.Replace(" ", "");
    }
}


