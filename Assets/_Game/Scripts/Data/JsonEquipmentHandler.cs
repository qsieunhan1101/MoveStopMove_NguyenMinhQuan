using System.Collections.Generic;
using UnityEngine;


public class JsonEquipmentHandler : MonoBehaviour
{
    private string filePath;
    private void Awake()
    {
        filePath = Application.dataPath + "/jsonEquipmentData.json";
        //filePath = Application.persistentDataPath + "/jsonEquipmentData.json";

        //filePath = "Assets/Resources/jsonEquipmentData.json";
        //filePath = "Assets/Resources/jsonEquipmentData.text";
        Debug.Log(filePath);
    }

    public void SaveDataEquipment(PlayerEquipmentData playerEquipment)
    {
        string json = JsonUtility.ToJson(playerEquipment, true);
        //File.WriteAllText(filePath, json);
        PlayerPrefs.SetString(Constant.PlayerPref_Key_Equipment, json);
        PlayerPrefs.Save();
    }
    public PlayerEquipmentData LoadDataEquipment()
    {
        //string json = File.ReadAllText(filePath);

        if (PlayerPrefs.HasKey(Constant.PlayerPref_Key_Equipment))
        {
            string json = PlayerPrefs.GetString(Constant.PlayerPref_Key_Equipment);
            PlayerEquipmentData playerEquipmentData = JsonUtility.FromJson<PlayerEquipmentData>(json);
            return playerEquipmentData; 
        }

        return new PlayerEquipmentData();
    }

}

public class PlayerEquipmentData
{
    public DictionaryWapper<string, int> dictionartHat;
    public DictionaryWapper<string, int> dictionartPant;
    public DictionaryWapper<string, int> dictionartShield;
    public DictionaryWapper<string, int> dictionartSkin;
    public int idListEquipment;
    public string equipmentName;
    public int level;
    public PlayerEquipmentData()
    {
        dictionartHat = new DictionaryWapper<string, int>();
        dictionartPant = new DictionaryWapper<string, int>();
        dictionartShield = new DictionaryWapper<string, int>();
        dictionartSkin = new DictionaryWapper<string, int>();
        idListEquipment = 0;
        equipmentName = "Arrow";
        level = 0;
    }

    public DictionaryWapper<string, int> UpdateDictionaryWapper(int dictLocation)
    {
        switch (dictLocation)
        {
            case 0:
                return dictionartHat;
            case 1:
                return dictionartPant;
            case 2:
                return dictionartShield;
            case 3:
                return dictionartSkin;
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
    public void UpdateElement(TKey key, TValue value)
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
