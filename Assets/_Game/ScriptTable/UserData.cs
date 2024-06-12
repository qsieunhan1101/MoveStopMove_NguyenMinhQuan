using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public enum WeaponType
{
    Hammer = 0,
    Lolipop = 1,
    Knife = 2,
    CandyCane = 3,
    Bomerang = 4,
    SwirlyPop = 5,
    Axe = 6,
    IceScream = 7,
    BattleAxe = 8,
    Arrow = 9,
    Uzi = 10,
}

public enum HatType
{
    Arrow = 0,
    Cowboy = 1,
    Crown = 2,
    Ear = 3,
    Hat = 4,
    Hat_Cap = 5,
    Hat_Yellow = 6,
    Head_Phone = 7,
    Horn = 8,
    Rau = 9,

}

[CreateAssetMenu(fileName = "New User Data", menuName = "UserData")]

public class UserData : ScriptableObject
{
    [Header ("Weapon")]
    [SerializeField] private List<WeaponData> listWeaponDatas;


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
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
        return ListHatDatas[1];
    }

    public HatData GetHatEquipmentData(string euipmentName)
    {
        for (int i = 0; i<ListHatDatas.Count-1;i++)
        {
            if (ListHatDatas[i].equipmentName == euipmentName)
            {
                return ListHatDatas[i];
            }
        }
        return ListHatDatas[1];
    }

    //Pant


    //Shield

    //Skin
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

}

