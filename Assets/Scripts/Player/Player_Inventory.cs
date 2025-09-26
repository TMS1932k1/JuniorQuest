using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : Entity_Inventory, ISaveable
{
    public static event Action OnUpdateInventory;

    [SerializeField] ListPickUpSO listPickUpSO;

    private Player_SFX playerSFX;


    private void Awake()
    {
        playerSFX = GetComponent<Player_SFX>();
    }

    public override void AddToInventory(ObjectPickUpSO pickUpData)
    {
        base.AddToInventory(pickUpData);

        playerSFX.PlayPickUp();
        OnUpdateInventory?.Invoke();
    }

    public override ObjectPickUpSO GetOutInventory(string pickUpName)
    {
        ObjectPickUpSO pickUpData = base.GetOutInventory(pickUpName);
        OnUpdateInventory?.Invoke();

        return pickUpData;
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load Inventory of Player");

        inventory.Clear();

        foreach (KeyValuePair<string, int> pair in gameData.inventory)
        {
            ObjectPickUpSO pickUpData = listPickUpSO.GetPickUpWithSaveID(pair.Key);

            if (pickUpData == null)
                continue;

            Slot slot = new Slot(pickUpData);
            slot.stack = pair.Value;

            inventory.Add(slot);
        }

        OnUpdateInventory?.Invoke();
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save Inventory of Player");

        gameData.inventory.Clear();

        foreach (Slot slot in inventory)
            gameData.inventory.Add(slot.data.saveID, slot.stack);
    }
}
