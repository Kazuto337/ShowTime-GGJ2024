using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveHighScore(float newHighScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/HighScore.Save";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        PlayerData data = new PlayerData(newHighScore);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadHighScore()
    {
        string path = Application.persistentDataPath + "/HighScore.Save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File Not Found!");
            PlayerData data = new PlayerData(0f);
            return data;
        }
    }
}
