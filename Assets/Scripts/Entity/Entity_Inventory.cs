using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public ObjectPickUpSO data;
    public int stack;

    public Slot(ObjectPickUpSO data)
    {
        this.data = data;
        stack = 1;
    }
}

public class Entity_Inventory : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] protected List<Slot> inventory = new();
    [SerializeField] int slotCount;


    public bool IsFull() => inventory.Count >= slotCount;
    public List<Slot> GetInventory() => inventory;
    public int GetSlotCount() => slotCount;

    public virtual void AddToInventory(ObjectPickUpSO pickUpData)
    {
        if (IsFull())
            return;

        Slot slot = FindPickUp(pickUpData.pickUpName);
        if (slot != null)
            slot.stack++;
        else
            inventory.Add(new Slot(pickUpData));
    }

    public virtual ObjectPickUpSO GetOutInventory(string pickUpName)
    {
        Slot slot = FindPickUp(pickUpName);

        if (slot == null)
            return null;

        if (slot.stack > 1)
            slot.stack--; // Decrement stack
        else
            inventory.Remove(slot); // Remove this slot

        return slot.data;
    }

    public Slot FindPickUp(string pickUpName)
    {
        return inventory.FirstOrDefault(obj => obj.data.pickUpName == pickUpName);
    }
}
