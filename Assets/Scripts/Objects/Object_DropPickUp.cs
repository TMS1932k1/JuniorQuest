using UnityEngine;

public class Object_DropPickUp : MonoBehaviour, ISaveable
{
    [Header("Data")]
    [SerializeField] ObjectPickUpSO data;


    // Components
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    protected ParticleSystem auraPs;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        auraPs = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        var main = auraPs.main;
        main.startColor = Color.purple;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity_Inventory inventory = collision.gameObject.GetComponent<Entity_Inventory>();

        if (inventory && !inventory.IsFull())
        {
            inventory.AddToInventory(data);
            Destroy(gameObject);
        }
    }

    public void SetData(ObjectPickUpSO data)
    {
        if (data == null)
            return;

        this.data = data;

        sr.sprite = data.image;
        gameObject.name = "DropPickUp_" + data.pickUpName.Replace(" ", "");
    }

    public void SetVelocity(float x, float y)
    {
        rb.linearVelocity = new Vector2(x, y);
    }

    public void SaveData(ref GameData gameData)
    {

    }

    public void LoadData(GameData gameData)
    {

    }
}
