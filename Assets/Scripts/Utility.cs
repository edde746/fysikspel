using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[Serializable]
class GameData
{
    [SerializeField]
    public List<string> Completed = new List<string>();
    private static string path = Path.Combine(Application.persistentDataPath, "save.json");

    public void SaveToFile()
    {
        File.WriteAllText(path, JsonUtility.ToJson(this));
    }

    public static GameData Load()
    {
        if (File.Exists(path))
            return JsonUtility.FromJson<GameData>(File.ReadAllText(path));
        else
            return new GameData();
    }
}

class Utility
{
    public static GameData Savefile = GameData.Load();
}