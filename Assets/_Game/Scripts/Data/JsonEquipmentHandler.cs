using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class JsonEquipmentHandler : MonoBehaviour
{
    private string filePath;
    private void Awake()
    {
        filePath = Application.dataPath + "/jsonEquipmentData.json";
    }

    public void SaveDataEquipment(PlayerEquipmentData playerEquipment)
    {
        string json = JsonUtility.ToJson(playerEquipment, true);
        File.WriteAllText(filePath, json);
    }
    public PlayerEquipmentData LoadDataEquipment()
    {
        string json = File.ReadAllText(filePath);
        PlayerEquipmentData playerEquipmentData = JsonUtility.FromJson<PlayerEquipmentData>(json);

        return playerEquipmentData;
    }

}

public class PlayerEquipmentData
{
    public DictionaryWapper<string, int> dictionartHat;
    public int idListEquipment;
    public string equipmentName;
    public PlayerEquipmentData()
    {
        dictionartHat = new DictionaryWapper<string, int>();
        idListEquipment = 0;
        equipmentName = "Arrow";
    }

    public DictionaryWapper<string, int> UpdateDictionaryWapper(int dictLocation)
    {
        switch (dictLocation)
        {
            case 0:
                return dictionartHat;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
        return dictionartHat;
    }
}


[System.Serializable]
public class DictionaryWapper<TKey, TValue>
{
    public List<TKey> keys = new List<TKey>();
    public List<TValue> values = new List<TValue>();

    public void AddElement(TKey key, TValue value)
    {
        keys.Add(key);
        values.Add(value);
    }
    public void UpdateElement(TKey key ,TValue value)
    {
        int index = keys.IndexOf(key);
        if (index >= 0)
        {
            values[index] = value;
        }
        else
        {
           AddElement(key, value);
        }
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dict[keys[i]] = values[i];
        }

        return dict;
    }

    public void FromDictionary(Dictionary<TKey, TValue> dict)
    {
        keys.Clear();
        values.Clear();
        foreach (var kvp in dict)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }
}