using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Pick Up Setup/Create new ListPickUp Data", fileName = "PICK_UP_LIST")]
public class ListPickUpSO : ScriptableObject
{
    public ObjectPickUpSO[] pickUpList;


    public ObjectPickUpSO GetPickUpWithSaveID(string saveID)
    {
        return pickUpList.FirstOrDefault(pickUp => pickUp != null && pickUp.saveID == saveID);
    }
}
