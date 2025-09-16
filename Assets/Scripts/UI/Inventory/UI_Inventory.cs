using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] Player_Inventory playerInventory;
    [SerializeField] UI_PickUp pickUpPrefab;

    private List<UI_PickUp> pickUpList = new();


    void Awake()
    {
        Player_Inventory.OnUpdateInventory += SetDisplay;
    }

    void Start()
    {
        // Instantiate empty pickUp UI
        for (int i = 0; i < playerInventory.GetSlotCount(); i++)
        {
            UI_PickUp pickUpUI = Instantiate(pickUpPrefab, transform);
            pickUpList.Add(pickUpUI);
        }

        SetDisplay();
    }

    void OnDestroy()
    {
        Player_Inventory.OnUpdateInventory += SetDisplay;
    }

    private void SetDisplay()
    {
        for (int i = 0; i < pickUpList.Count; i++)
        {
            if (i < playerInventory.GetInventory().Count)
                pickUpList[i].SetDisplay(playerInventory.GetInventory()[i]);
            else
                pickUpList[i].SetEmpty();
        }
    }
}
