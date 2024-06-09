using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataController : Singleton<PlayerDataController>
{
    [SerializeField] private JsonHandler jsonHandler;


    private const string FirstRunKey = "IsFirstRun";

    public PlayerData playerData = new PlayerData((new Dictionary<WeaponType, int>(), new int()));

    private void Awake()
    {

    }
    private void Start()
    {






        if (IsFirstRun())
        {
            for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length - 1; i++)
            {
                playerData.weaponPurchaseState.Add((WeaponType)i, 0);
            }
            jsonHandler.SaveDataToJson(playerData.weaponPurchaseState, 0);

            Debug.Log("Lan dau tien chay game");
            PlayerPrefs.SetInt(FirstRunKey, 0);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Khong phai lan dau tien chay game");

        }



        playerData = new PlayerData(jsonHandler.LoadDataFromJson());

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