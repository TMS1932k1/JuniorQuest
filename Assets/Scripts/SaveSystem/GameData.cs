using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // PLAYER DATA
    public int playerLevel;
    public float playerXP;
    public float playerHealth;
    public float strength;
    public float agility;
    public float vitality;
    public List<string> installedSkills = new();
    public SerializationDictionary<string, int> inventory = new();
    public Vector3 position;
    public string questId;


    // ENTITIES DATA
    public SerializationDictionary<string, bool> entities = new();


    // INTERACTABLE DATA
    public SerializationDictionary<string, bool> interactables = new();


    // ELEVATOR DATA
    public SerializationDictionary<string, bool> elevators = new();


    // QUEST DATA
    public string currentQuestId;
    public SerializationDictionary<string, int> targetGoaleds = new();
    public SerializationDictionary<string, bool> allQuestStatus = new();


    // SCENE DATA
    public string sceneName;


    // AUDIO SETTINGS DATA
    public float effectsValue = AudioSettingsValues.DEFAULT_VALUE;
    public float uiValue = AudioSettingsValues.DEFAULT_VALUE;
    public float bgmValue = AudioSettingsValues.DEFAULT_VALUE;
}


[System.Serializable]
public class SerializationDictionary<T, V> : Dictionary<T, V>, ISerializationCallbackReceiver
{
    [SerializeField] List<T> keys = new();
    [SerializeField] List<V> values = new();

    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            Debug.Log("Count of keys isn't equal count of values");
            return;
        }

        for (int i = 0; i < keys.Count; i++)
            Add(keys[i], values[i]);
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (KeyValuePair<T, V> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
}