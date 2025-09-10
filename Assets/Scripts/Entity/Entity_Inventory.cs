using System;
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
    public static event Action OnUpdateInventory;


    [Header("Details")]
    public List<Slot> inventory { get; private set; } = new();
    [SerializeField] int slotCount;


    public bool isFull() => inventory.Count >= slotCount;

    public int GetSlotCount() => slotCount;

    public void AddToInventory(ObjectPickUpSO pickUpData)
    {
        if (isFull())
            return;

        Slot slot = FindPickUp(pickUpData.pickUpName);
        if (slot != null)
            slot.stack++;
        else
            inventory.Add(new Slot(pickUpData));

        OnUpdateInventory?.Invoke();
    }

    public ObjectPickUpSO GetOutInventory(string pickUpName)
    {
        Slot slot = FindPickUp(pickUpName);

        if (slot == null)
            return null;

        if (slot.stack > 1)
            slot.stack--; // Decrement stack
        else
            inventory.Remove(slot); // Remove this slot

        OnUpdateInventory?.Invoke();
        return slot.data;
    }

    public Slot FindPickUp(string pickUpName)
    {
        return inventory.FirstOrDefault(obj => obj.data.pickUpName == pickUpName);
    }
}
