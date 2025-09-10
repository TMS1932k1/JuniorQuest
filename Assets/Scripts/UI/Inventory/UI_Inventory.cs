using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] Entity_Inventory entityInventory;
    [SerializeField] UI_PickUp pickUpPrefab;

    private List<UI_PickUp> pickUpList = new();


    void Awake()
    {
        for (int i = 0; i < entityInventory.GetSlotCount(); i++)
        {
            UI_PickUp pickUpUI = Instantiate(pickUpPrefab, transform);
            pickUpList.Add(pickUpUI);
        }

        Entity_Inventory.OnUpdateInventory += SetDisplay;
    }

    void Start()
    {
        SetDisplay();
    }

    void OnDestroy()
    {
        Entity_Inventory.OnUpdateInventory -= SetDisplay;
    }

    private void SetDisplay()
    {
        for (int i = 0; i < pickUpList.Count; i++)
        {
            if (i < entityInventory.inventory.Count)
                pickUpList[i].SetDisplay(entityInventory.inventory[i]);
            else
                pickUpList[i].SetEmpty();
        }
    }
}
