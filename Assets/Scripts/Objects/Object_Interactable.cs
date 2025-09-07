using UnityEngine;

public abstract class Object_Interactable : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveRange;


    // Components
    protected SpriteRenderer sr;
    protected ParticleSystem auraPs;


    private Vector2 originPosition;
    protected Color effectColor;
    protected bool isTaked;


    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        auraPs = GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void Start()
    {
        effectColor = Color.white;
        originPosition = transform.position;
        ChangeColorPs(auraPs, effectColor);
    }

    protected virtual void Update()
    {
        if (!isTaked)
            moveVertical();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        HideObject();
    }

    private void moveVertical()
    {
        float y = originPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector2(transform.position.x, y);
    }

    protected void ChangeColorPs(ParticleSystem ps, Color color)
    {
        var main = ps.main;
        main.startColor = color;
    }

    protected virtual void HideObject()
    {
        isTaked = true;
        sr.color = Color.clear;
        auraPs.Stop();
    }
}
