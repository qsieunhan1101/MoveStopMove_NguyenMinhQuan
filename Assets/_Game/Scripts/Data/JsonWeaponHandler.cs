using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonWeaponHandler : MonoBehaviour
{

    private string filePath;

    private void Awake()
    {
        filePath = Application.dataPath + "/jsonWeaponData.json";
        //filePath = Path.Combine(Application.persistentDataPath, "jsonTestData.json");
        Debug.Log(filePath);
    }
    
    public void SaveDataToJson(Dictionary<WeaponType, int> dic, int gol)
    {
        string json = JsonUtility.ToJson(new SerializableDictionary(dic, gol), true);
        File.WriteAllText(filePath, json);
    }

    public (Dictionary<WeaponType, int>, int) LoadDataFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SerializableDictionary serializableDictionary = JsonUtility.FromJson<SerializableDictionary>(json);
            return (serializableDictionary.ToDictionary(), serializableDictionary.golds);
        }

        return (new Dictionary<WeaponType, int>(), new int());
    }

}


[System.Serializable]
public class SerializableDictionary
{
    public List<string> keys;
    public List<int> values;
    public int golds;

    public SerializableDictionary(Dictionary<WeaponType, int> dictionary, int golds)
    {
        this.keys = new List<string>();
        this.values = new List<int>();
        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key.ToString());
            values.Add(kvp.Value);
        }

        this.golds = golds;
    }

    public Dictionary<WeaponType, int> ToDictionary()
    {
        Dictionary<WeaponType, int> dict = new Dictionary<WeaponType, int>();

        for (int i = 0; i < keys.Count; i++)
        {
            WeaponType key = (WeaponType)System.Enum.Parse(typeof(WeaponType), keys[i]);
            dict[key] = values[i];
        }
        return dict;
    }
}
