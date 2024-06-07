
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

[CreateAssetMenu(fileName = "New User Data", menuName = "UserData")]

public class UserData : ScriptableObject
{
    [SerializeField] private List<WeaponData> listWeaponDatas;


    //method
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
