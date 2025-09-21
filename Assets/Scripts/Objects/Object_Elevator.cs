using System;
using TMPro;
using UnityEngine;

public class Object_Elevator : MonoBehaviour, ISaveable
{
    [SerializeField] string uniqueId;

    [Space]
    [SerializeField] bool isActivity;
    [SerializeField] float speed;
    [SerializeField] ObjectPickUpSO pickUpData;
    [SerializeField] TextMeshProUGUI ActiveText;


    private Entity_Inventory inventory;
    private Vector2 originPosition;
    private int dirMove;


    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }

    private void Start()
    {
        ActiveText.text = $"Need {pickUpData.pickUpName}";
        ActiveText.gameObject.SetActive(false);
        originPosition = transform.position;
        dirMove = -1;
    }

    private void Update()
    {
        HandleInput();
        HandleMove();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && canActive())
        {
            inventory.GetOutInventory(pickUpData.pickUpName);
            isActivity = true;
        }
    }

    private void HandleMove()
    {
        if (isActivity)
        {
            if (transform.position.y >= originPosition.y && dirMove == 1)
                dirMove = -1;

            transform.position += Vector3.up * dirMove * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.GROUND_LAYER))
            dirMove = 1;

        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER) && !isActivity)
        {
            ActiveText.gameObject.SetActive(true);
            inventory = collision.gameObject.GetComponent<Entity_Inventory>();

            if (canActive())
                ActiveText.text = $"F: {pickUpData.pickUpName}";
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER))
        {
            inventory = null;
            ActiveText.gameObject.SetActive(false);
        }
    }

    private bool canActive()
    {
        return inventory != null && inventory.FindPickUp(pickUpData.pickUpName) != null;
    }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        if (!gameData.elevators.ContainsKey(uniqueId))
        {
            Debug.Log($"SAVE_MANAGER: Save {gameObject.name} ({uniqueId})");
            gameData.elevators.Add(uniqueId, isActivity);
        }
        else
        {
            Debug.Log($"SAVE_MANAGER: Update {gameObject.name} ({uniqueId})");
            gameData.elevators[uniqueId] = isActivity;
        }
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load {gameObject.name} ({uniqueId})");
        isActivity = gameData.elevators[uniqueId];
    }
}
