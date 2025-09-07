using UnityEngine;

[CreateAssetMenu(menuName = "Create new ObjectPickUp Data", fileName = "ObjectPickUpData_New")]
public class ObjectPickUpSO : ScriptableObject
{
    public Sprite image;
    public string pickUpName;
}
