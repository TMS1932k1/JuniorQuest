using System;
using UnityEngine;

public class Player_Inventory : Entity_Inventory
{
    public static event Action OnUpdateInventory;

    public override void AddToInventory(ObjectPickUpSO pickUpData)
    {
        base.AddToInventory(pickUpData);

        OnUpdateInventory?.Invoke();
    }

    public override ObjectPickUpSO GetOutInventory(string pickUpName)
    {
        OnUpdateInventory?.Invoke();
        return base.GetOutInventory(pickUpName);
    }
}
