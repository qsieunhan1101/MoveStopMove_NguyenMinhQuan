using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerDataManager : Singleton<PlayerDataManager>
{

    [SerializeField] private JsonWeaponHandler jsonHandler;
    [SerializeField] private JsonEquipmentHandler jsonEquipmentHandler;

    public PlayerWeaponData playerWeaponData = new PlayerWeaponData((new Dictionary<WeaponType, int>(), new int()));
    public PlayerEquipmentData playerEquipmentData = new PlayerEquipmentData();

    public int defaultGold;

    private void Start()
    {
        playerWeaponData = new PlayerWeaponData((new Dictionary<WeaponType, int>(), new int()));
        playerEquipmentData = new PlayerEquipmentData();


        if (IsFirstRun())
        {
            FirstGameRunData();
            Debug.Log("Lan dau tien chay game");
        }
        else
        {
            Debug.Log("Khong phai lan dau tien chay game");

        }


        //load PlayerData_Weapon
        playerWeaponData = LoadDataPlayer();
        //Load PlayerEquimentData
        playerEquipmentData = LoadPlayerEquipmentData();

        /*
                foreach (var item in playerData.weaponPurchaseState)
                {
                    Debug.Log(item.Key + ": " + item.Value);
                }*/

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

            jsonHandler.SaveDataToJson(playerWeaponData.weaponPurchaseState, 10000);
            playerWeaponData = LoadDataPlayer();

        }

    }

    private bool IsFirstRun()
    {
        return !PlayerPrefs.HasKey(Constant.PlayerPref_Key_FirstRun);
    }

    private void FirstGameRunData()
    {
        //khoi tao gia tri weapon lan dau tien chay
        for (int i = 0; i < LocalDataManager.Instance.UserData.ListWeaponData.Count; i++)
        {
            if (i == 0)
            {
                playerWeaponData.weaponPurchaseState.Add((WeaponType)i, 2);
            }
            else
            {
                playerWeaponData.weaponPurchaseState.Add((WeaponType)i, 0);

            }
        }
        jsonHandler.SaveDataToJson(playerWeaponData.weaponPurchaseState, defaultGold);


        //khoi tao gia tri equipment Hat lan dau tien chay
        for (int i = 0; i < LocalDataManager.Instance.UserData.ListHatDatas.Count; i++)
        {
            playerEquipmentData.dictionartHat.AddElement(LocalDataManager.Instance.UserData.ListHatDatas[i].equipmentName, 0);
        }


        //khoi tao gia tri equipment Pant lan dau tien chay
        for (int i = 0; i < LocalDataManager.Instance.UserData.ListPantDatas.Count; i++)
        {
            playerEquipmentData.dictionartPant.AddElement(LocalDataManager.Instance.UserData.ListPantDatas[i].equipmentName, 0);
        }

        //khoi tao gia tri equipment Shield lan dau tien chay
        for (int i = 0; i < LocalDataManager.Instance.UserData.ListShieldDatas.Count; i++)
        {
            playerEquipmentData.dictionartShield.AddElement(LocalDataManager.Instance.UserData.ListShieldDatas[i].equipmentName, 0);
        }
        for (int i = 0; i < LocalDataManager.Instance.UserData.ListSkinDatas.Count; i++)
        {
            playerEquipmentData.dictionartSkin.AddElement(LocalDataManager.Instance.UserData.ListSkinDatas[i].equipmentName, 0);
        }

        playerEquipmentData.equipmentName = Constant.Default_EquipmentName;
        jsonEquipmentHandler.SaveDataEquipment(playerEquipmentData);

        //luu trang thai lan dau chay game
        PlayerPrefs.SetInt(Constant.PlayerPref_Key_FirstRun, 0);
        PlayerPrefs.Save();
    }
    /////////////
    public void UpdateDataPlayer()
    {
        jsonHandler.SaveDataToJson(playerWeaponData.weaponPurchaseState, playerWeaponData.golds);
        playerWeaponData = new PlayerWeaponData(jsonHandler.LoadDataFromJson());
    }

    public PlayerWeaponData LoadDataPlayer()
    {
        return new PlayerWeaponData(jsonHandler.LoadDataFromJson());
    }

    public WeaponType GetWeaponState()
    {
        PlayerWeaponData p = new PlayerWeaponData(jsonHandler.LoadDataFromJson());

        for (int i = 0; i < p.weaponPurchaseState.Count; i++)
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
        int id = LoadPlayerEquipmentData().idListEquipment;
        string name = LoadPlayerEquipmentData().equipmentName;

        return (id, name);
    }
}



[System.Serializable]

public class PlayerWeaponData : MonoBehaviour
{
    public Dictionary<WeaponType, int> weaponPurchaseState;
    public int golds;


    public PlayerWeaponData((Dictionary<WeaponType, int>, int) dataFromJson)
    {
        this.weaponPurchaseState = dataFromJson.Item1;
        this.golds = dataFromJson.Item2;
    }

}