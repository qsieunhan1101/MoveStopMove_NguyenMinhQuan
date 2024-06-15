using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabControl : MonoBehaviour
{
    public enum TabType
    {
        TabHat = 0,
        TabPant = 1,
        TapShield = 2,
        TapSkin = 3,
    }
    [SerializeField] private TabType tabType;

    [Header("Button")]
    [SerializeField] private Button btnSelect_Equipped;
    [SerializeField] private Button btnBuy;

    [Header ("ButtonItem")]
    [SerializeField] private GameObject buttonPrefabs;
    [SerializeField] private Transform buttonParent;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textSelect_Equipped;

    private int idPlayerEquipmentLocation;




    [SerializeField] private List<EquipmentData> equipments;


    // Start is called before the first frame update
    void Start()
    {
        GenerateItem();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateItem()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
        CheckTabType();
        for (int i = 0; i < equipments.Count; i++)
        {
            GameObject btn = Instantiate(buttonPrefabs, buttonParent);
            SkinItemUI itemData = btn.GetComponent<SkinItemUI>();
            //set data cho button con
            itemData.btnBuy = this.btnBuy;
            itemData.btnSelect_Equipped = this.btnSelect_Equipped;
            itemData.textPrice = this.textPrice;
            itemData.textSelect_Equipped = this.textSelect_Equipped;

            itemData.itemEquipmentData = this.equipments[i];

            itemData.idPlayerEquipmentLocation = this.idPlayerEquipmentLocation;
        }
    }

    private void CheckTabType()
    {
        if (tabType == TabType.TabHat)
        {
            equipments = LocalDataManager.Instance.UserData.ListHatDatas.Cast<EquipmentData>().ToList();
            idPlayerEquipmentLocation = 0;
        }
        if (tabType == TabType.TabPant)
        {
            equipments = LocalDataManager.Instance.UserData.ListPantDatas.Cast<EquipmentData>().ToList();
            idPlayerEquipmentLocation = 1;

        }
        if (tabType == TabType.TapShield)
        {
            equipments = LocalDataManager.Instance.UserData.ListShieldDatas.Cast<EquipmentData>().ToList();
            idPlayerEquipmentLocation = 2;
        }
        if (tabType == TabType.TapSkin)
        {
            equipments = LocalDataManager.Instance.UserData.ListSkinDatas.Cast<EquipmentData>().ToList();
            idPlayerEquipmentLocation = 3;
        }
    }
}
