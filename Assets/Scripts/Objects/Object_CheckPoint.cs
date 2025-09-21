using TMPro;
using UnityEngine;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    [SerializeField] TextMeshProUGUI saveText;
    [SerializeField] SpriteRenderer lightOn;


    private bool isShowText;
    private Object_CheckPoint[] checkPoints;
    public bool isSaved;


    private void Awake()
    {
        checkPoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
    }

    private void Start()
    {
        isShowText = false;
        saveText.gameObject.SetActive(isShowText);
        EnableSave(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isShowText)
        {
            OffAllCheckPoint();

            EnableSave(true);
            EnableSaveText(false);

            SaveManager.instance.SaveGame();
        }
    }

    public void EnableSave(bool enable)
    {
        isSaved = enable;
        lightOn.gameObject.SetActive(enable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER) && !isShowText)
        {
            EnableSaveText(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER))
        {
            EnableSaveText(false);
        }
    }

    private void OffAllCheckPoint()
    {
        foreach (Object_CheckPoint checkPoint in checkPoints)
        {
            checkPoint.EnableSave(false);
        }
    }

    private void EnableSaveText(bool enable)
    {
        isShowText = enable;
        saveText.gameObject.SetActive(enable);
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save {gameObject.name} ({transform.position})");

        if (isSaved)
            gameData.position = transform.position;
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load {gameObject.name} ({transform.position})");
        if (gameData.position == transform.position)
        {
            EnableSave(true);
            EnableSaveText(false);
        }
    }
}
