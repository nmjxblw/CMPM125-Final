using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float health;
    public bool isLookingBoar;
    public bool isLookingSoldier;
    public bool isLookingRanger;
    public GameObject currentPlayer;
    
}

public class SaveLoadManager
{
    private static string dataPath = Path.Combine(Application.persistentDataPath, "playerData.json");

    public static void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return new PlayerData();
    }
}
