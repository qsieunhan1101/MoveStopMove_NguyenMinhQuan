using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New User Data", menuName = "UserData")]

public class UserData : ScriptableObject
{
    [Header("Weapon")]
    [SerializeField] private List<WeaponData> listWeaponDatas;
    public List<WeaponData> ListWeaponData => listWeaponDatas;


    [Header("Hat")]
    [SerializeField] private List<HatData> listHatDatas;
    public List<HatData> ListHatDatas => listHatDatas;


    [Header("Pant")]
    [SerializeField] private List<PantData> listPantDatas;
    public List<PantData> ListPantDatas => listPantDatas;


    [Header("Shield")]
    [SerializeField] private List<ShieldData> listShieldDatas;
    public List<ShieldData> ListShieldDatas => listShieldDatas;


    [Header("Skin")]

    [SerializeField] private List<SkinData> listSkinDatas;
    public List<SkinData> ListSkinDatas => listSkinDatas;

    [SerializeField] private List<string> listNames;
    public List<string> ListNames => listNames;

    //Weapon
    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        for (int i = 0; i < listWeaponDatas.Count - 1; i++)
        {
            if (weaponType == listWeaponDatas[i].weaponType)
            {
                return listWeaponDatas[i];
            }
        }
        return listWeaponDatas[listWeaponDatas.Count - 1];
    }

    //Hat


    public EquipmentData GetEquipmentData(int idListEquipment, string equipmentName)
    {
        switch (idListEquipment)
        {
            case 0:
                return GetHatEquipmentData(equipmentName);
            case 1:
                return GetPantEquipmentData(equipmentName);
            case 2:
                return GetShieldEquipmentData(equipmentName);
            case 3:
                return GetSkinEquipmentData(equipmentName);
        }
        return ListHatDatas[1];
    }

    private HatData GetHatEquipmentData(string euipmentName)
    {
        for (int i = 0; i < ListHatDatas.Count; i++)
        {
            if (ListHatDatas[i].equipmentName == euipmentName)
            {
                return ListHatDatas[i];
            }
        }
        return ListHatDatas[1];
    }
    private PantData GetPantEquipmentData(string euipmentName)
    {
        for (int i = 0; i < ListPantDatas.Count; i++)
        {
            if (ListPantDatas[i].equipmentName == euipmentName)
            {
                return ListPantDatas[i];
            }
        }
        return ListPantDatas[1];
    }

    private ShieldData GetShieldEquipmentData(string euipmentName)
    {
        for (int i = 0; i < ListShieldDatas.Count; i++)
        {
            if (ListShieldDatas[i].equipmentName == euipmentName)
            {
                return ListShieldDatas[i];
            }
        }
        return ListShieldDatas[1];
    }

    private SkinData GetSkinEquipmentData(string euipmentName)
    {
        for (int i = 0; i < ListSkinDatas.Count; i++)
        {
            if (ListSkinDatas[i].equipmentName == euipmentName)
            {
                return ListSkinDatas[i];
            }
        }
        return ListSkinDatas[1];
    }

}

[System.Serializable]
public class WeaponData
{
    public WeaponType weaponType;
    public string weaponName;
    public Sprite weaponIcon;
    public int weaponPrice;
    public GameObject weaponPrefab;
    public GameObject weaponHand;
}

//
[System.Serializable]
public class EquipmentData
{
    public string equipmentName;
    public Sprite equipmentIcon;
    public int equipmentPrice;
}

[System.Serializable]
public class HatData : EquipmentData
{
    public GameObject hatPrefab;
}
[System.Serializable]
public class PantData : EquipmentData
{
    public Material pantMaterial;
}
[System.Serializable]
public class ShieldData : EquipmentData
{
    public GameObject shieldPrefab;
}
[System.Serializable]
public class SkinData : EquipmentData
{
    public Material skinMaterial;
    public Material pantMaterial;
    public GameObject skinPrefab;

}

