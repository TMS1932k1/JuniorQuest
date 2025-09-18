using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] string fileName = "filesave.json";
    [SerializeField] bool isEncryptDecrypt = false;


    private FileDataHandler fileDataHandler;
    private GameData gameData;
    private List<ISaveable> saveables;


    private IEnumerator Start()
    {
        Debug.Log(Application.persistentDataPath);
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        saveables = GetAllSaveable();

        yield return new WaitForSeconds(0.1f);
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        foreach (ISaveable saveable in saveables)
            saveable.SaveDate(ref gameData);

        fileDataHandler.SaveData(gameData, isEncryptDecrypt);
    }

    public void LoadGame()
    {
        gameData = fileDataHandler.LoadData(isEncryptDecrypt);

        if (gameData == null)
        {
            Debug.Log("Not found save data");
            gameData = new GameData();
            return;
        }

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
