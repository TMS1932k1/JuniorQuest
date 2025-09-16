using UnityEngine;

public class Boss_Inventory : Entity_Inventory
{
    [SerializeField] Object_DropPickUp dropPickUpPrefab;

    public void DropAllInventory()
    {
        int count = 0;

        foreach (Slot slot in inventory)
        {
            count++;
            ObjectPickUpSO data = slot.data;

            for (int i = 0; i < slot.stack; i++)
            {
                count++;
                Object_DropPickUp obj = Instantiate(dropPickUpPrefab);

                // Set data
                obj.SetData(data);

                // Set drop velocity
                obj.gameObject.transform.position = transform.position;
                obj.SetVelocity(2 * count, 2 * count);
            }
        }
    }
}
