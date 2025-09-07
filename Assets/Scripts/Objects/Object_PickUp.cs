using UnityEngine;

public class Object_PickUp : Object_Interactable
{
    [Header("Data")]
    [SerializeField] ObjectPickUpSO data;

    protected override void Start()
    {
        base.Start();

        ChangeColorPs(auraPs, Color.purple);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTaked) return;

        Entity_Inventory inventory = collision.GetComponent<Entity_Inventory>();

        if (inventory && !inventory.isFull())
        {
            inventory.AddToInventory(data);
            base.OnTriggerEnter2D(collision);
        }

    }

    protected override void HideObject()
    {
        base.HideObject();

        Destroy(gameObject);
    }

    void OnValidate()
    {
        if (data == null)
            return;

        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = data.image;

        gameObject.name = "PickUp_" + data.pickUpName.Replace(" ", "");
    }
}
