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
    [SerializeField] List<Slot> inventory;
    [SerializeField] int slotCount;


    public bool isFull() => inventory.Count >= slotCount;

    public void AddToInventory(ObjectPickUpSO pickUpData)
    {
        if (isFull())
            return;

        Slot slot = inventory.FirstOrDefault(s => s.data.pickUpName == pickUpData.pickUpName);
        if (slot != null)
            slot.stack++;
        else
            inventory.Add(new Slot(pickUpData));
    }

    public ObjectPickUpSO GetOutInventory(string pickUpName)
    {
        Slot slot = inventory.FirstOrDefault(obj => obj.data.pickUpName == pickUpName);

        if (slot != null && slot.stack > 1)
            slot.stack--;
        else
            inventory.Remove(slot);

        return slot.data;
    }
}
