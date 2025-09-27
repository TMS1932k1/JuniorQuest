using System;
using UnityEngine;

public abstract class Object_Interactable : MonoBehaviour, ISaveable
{
    [SerializeField] string uniqueId;

    [Space]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveRange;


    // Components
    protected SpriteRenderer sr;
    protected ParticleSystem auraPs;


    private Vector2 originPosition;
    protected Color effectColor;
    public bool isTaked { get; protected set; }


    protected virtual void Awake()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }

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

    private void HideObject()
    {
        isTaked = true;
        gameObject.SetActive(false);
    }

    public void SaveData(ref GameData gameData)
    {
        if (!gameData.interactables.ContainsKey(uniqueId))
        {
            Debug.Log($"SAVE_MANAGER: Save {gameObject.name} ({uniqueId})");
            gameData.interactables.Add(uniqueId, isTaked);
        }
        else
        {
            Debug.Log($"SAVE_MANAGER: Update {gameObject.name} ({uniqueId})");
            gameData.interactables[uniqueId] = isTaked;
        }
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load {gameObject.name} ({uniqueId})");

        if (!gameData.interactables.ContainsKey(uniqueId))
            return;

        if (gameData.interactables[uniqueId])
        {
            HideObject();
        }
        else
        {
            isTaked = false;
            gameObject.SetActive(true);
        }
    }

    protected virtual void OnValidate()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}
