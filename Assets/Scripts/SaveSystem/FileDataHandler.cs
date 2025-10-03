using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string fullPath;
    private string myKey = "tms";


    public FileDataHandler(string dataDirPath, string fileName)
    {
        fullPath = Path.Combine(dataDirPath, fileName);
    }

    public void SaveData(GameData data, bool isEncryptDecrypt)
    {
        try
        {
            // Create directopry if not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Convert data to json
            string dataSave = JsonUtility.ToJson(data);

            // Encrypt data if need
            if (isEncryptDecrypt)
                dataSave = XorCipher.EncryptToBase64(dataSave, myKey);

            // Open or create data to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // Write data to file
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error save data " + e.Message);
        }
    }

    public GameData LoadData(bool isEncryptDecrypt)
    {
        GameData data = null;

        // Check file exist
        if (File.Exists(fullPath))
        {
            try
            {
                // Open file
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    // Read data from file
                    using (StreamReader read = new StreamReader(stream))
                    {
                        string dataLoad = read.ReadToEnd();

                        // Decrypt data if need
                        if (isEncryptDecrypt)
                            dataLoad = XorCipher.DecryptFromBase64(dataLoad, myKey);

                        // Convert json to game data
                        data = JsonUtility.FromJson<GameData>(dataLoad);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error load data " + e.Message);
            }
        }

        return data;
    }

    public void DeleteData()
    {
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
