using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string fullPath;


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
                dataSave = EncryptDecrypt(dataSave);

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
            Debug.Log("Error save data " + e.Message);
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
                            dataLoad = EncryptDecrypt(dataLoad);

                        // Convert json to game data
                        data = JsonUtility.FromJson<GameData>(dataLoad);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error load data " + e.Message);
            }
        }

        return data;
    }

    public void DeleteData()
    {
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    /// <summary>
    /// Encrypt or decrypt data (XOR Cipher)
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private string EncryptDecrypt(string data)
    {
        return data;
    }
}
