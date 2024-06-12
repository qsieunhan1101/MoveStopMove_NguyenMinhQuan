using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerDataManager : Singleton<PlayerDataManager>
{
    [SerializeField] private JsonHandler jsonHandler;

    [SerializeField] private JsonEquipmentHandler jsonEquipmentHandler;


    private const string FirstRunKey = "IsFirstRun";

    public PlayerData playerData = new PlayerData((new Dictionary<WeaponType, int>(), new int()));

    public PlayerEquipmentData playerEquipmentData = new PlayerEquipmentData();

    private void Awake()
    {

    }
    private void Start()
    {

        /*for (int i =0; i< LocalDataManager.Instance.UserData.ListHatDatas.Count-1;i++)
        {
            playerEquipmentData.dictionartHat.AddElement(LocalDataManager.Instance.UserData.ListHatDatas[i].equipmentName, 0);
        }


        //test
        jsonEquipmentHandler.SaveDataEquipment(playerEquipmentData);
        playerEquipmentData.dictionartHat.UpdateElement(LocalDataManager.Instance.UserData.ListHatDatas[0].equipmentName, 1);
        playerEquipmentData.dictionartHat.UpdateElement(LocalDataManager.Instance.UserData.ListHatDatas[3].equipmentName, 2);
        jsonEquipmentHandler.SaveDataEquipment(playerEquipmentData);
        */



        //load playerEquipmentData
        //playerEquipmentData = LoadPlayerEquipmentData();


        playerData = new PlayerData((new Dictionary<WeaponType, int>(), new int()));

        if (IsFirstRun())
        {
            //khoi tao gia tri weapon lan dau tien chay
            for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length - 1; i++)
            {
                playerData.weaponPurchaseState.Add((WeaponType)i, 0);
            }
            jsonHandler.SaveDataToJson(playerData.weaponPurchaseState, 0);


            //khoi tao gia tri equipment lan dau tien chay
            for (int i = 0; i < LocalDataManager.Instance.UserData.ListHatDatas.Count - 1; i++)
            {
                playerEquipmentData.dictionartHat.AddElement(LocalDataManager.Instance.UserData.ListHatDatas[i].equipmentName, 0);
            }

            jsonEquipmentHandler.SaveDataEquipment(playerEquipmentData);


            Debug.Log("Lan dau tien chay game");
            PlayerPrefs.SetInt(FirstRunKey, 0);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Khong phai lan dau tien chay game");

        }


        //load PlayerData_Weapon
        playerData = LoadDataPlayer();
        //Load PlayerEquimentData
        playerEquipmentData = LoadPlayerEquipmentData();
        foreach (var item in playerData.weaponPurchaseState)
        {
            Debug.Log(item.Key + ": " + item.Value);
        }

    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Reset Data thanh cong");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {

            jsonHandler.SaveDataToJson(playerData.weaponPurchaseState, 1000);

        }

    }

    private bool IsFirstRun()
    {

        return !PlayerPrefs.HasKey(FirstRunKey);
    }
    /////////////
    public void UpdateDataPlayer()
    {
        jsonHandler.SaveDataToJson(playerData.weaponPurchaseState, playerData.golds);
        playerData = new PlayerData(jsonHandler.LoadDataFromJson());
    }

    public PlayerData LoadDataPlayer()
    {
        return new PlayerData(jsonHandler.LoadDataFromJson());
    }

    public WeaponType GetWeaponState()
    {
        PlayerData p = new PlayerData(jsonHandler.LoadDataFromJson());
        for (int i = 0; i < p.weaponPurchaseState.Count - 1; i++)
        {
            if (p.weaponPurchaseState[(WeaponType)i] == 2)
            {
                return p.weaponPurchaseState.Keys.ElementAt(i);
            }
        }
        return WeaponType.Hammer;
    }

    //////////////////
    public void UpdatePlayerEquipmentData()
    {
        jsonEquipmentHandler.SaveDataEquipment(playerEquipmentData);
        playerEquipmentData = jsonEquipmentHandler.LoadDataEquipment();
    }
    public PlayerEquipmentData LoadPlayerEquipmentData()
    {
        return jsonEquipmentHandler.LoadDataEquipment();
    }

    public (int, string) GetEquipmentState()
    {
        int a = LoadPlayerEquipmentData().idListEquipment;
        string b = LoadPlayerEquipmentData().equipmentName;

        return (a, b);
    }




}
[System.Serializable]

public class PlayerData : MonoBehaviour
{
    public Dictionary<WeaponType, int> weaponPurchaseState;

    public int golds;


    public PlayerData((Dictionary<WeaponType, int>, int) dataFromJson)
    {
        this.weaponPurchaseState = dataFromJson.Item1;
        this.golds = dataFromJson.Item2;
    }

}