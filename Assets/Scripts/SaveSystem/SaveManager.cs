using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    [Header("Setting Options")]
    [SerializeField] string fileName = "filesave.json";
    [SerializeField] bool isEncryptDecrypt = false;
    [SerializeField] bool isActive = true;


    private FileDataHandler fileDataHandler;
    private GameData gameData;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
        Debug.Log(Application.persistentDataPath);
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        yield return new WaitForSeconds(0.1f);
        LoadGame();
    }

    public void SaveGame()
    {
        if (!isActive)
            return;

        // gameData.entities.Clear();
        // gameData.interactables.Clear();
        // gameData.elevators.Clear();

        List<ISaveable> saveables = GetAllSaveable();
        foreach (ISaveable saveable in saveables)
            saveable.SaveData(ref gameData);

        fileDataHandler.SaveData(gameData, isEncryptDecrypt);
    }

    public void SavePosition(Vector3 position)
    {
        if (!isActive)
            return;

        gameData.position = position;
        fileDataHandler.SaveData(gameData, isEncryptDecrypt);
    }

    public void LoadGame()
    {
        if (!isActive)
            return;

        gameData = fileDataHandler.LoadData(isEncryptDecrypt);

        if (gameData == null)
        {
            Debug.Log("Not found save data");
            gameData = new GameData();
            return;
        }

        List<ISaveable> saveables = GetAllSaveable();
        foreach (ISaveable saveable in saveables)
            saveable.LoadData(gameData);
    }

    [ContextMenu("DELETE SAVE DATA")]
    public void DeleteData()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        fileDataHandler.DeleteData();
    }

    private List<ISaveable> GetAllSaveable()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OfType<ISaveable>()
            .ToList();
    }
}
