using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinItemUI : MonoBehaviour
{
    [Header("Button")]
    public Button btnItem;
    public Button btnSelect_Equipped;
    public Button btnBuy;


    [Header("Image")]
    [SerializeField] private Image btnIcon;

    [Header("Text")]
    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI textSelect_Equipped;

    [Header ("Data")]
    [SerializeField] private string itemEquippedName;
    private Dictionary<string, int> itemDictionaryData = new Dictionary<string, int>();

    public int idPlayerEquipmentLocation = 0;
    public EquipmentData itemEquipmentData;
    public static Action<int, string> itemOnclickEvent;





    // Start is called before the first frame update
    void Start()
    {
        btnItem = this.GetComponent<Button>();
        btnItem.onClick.AddListener(OnClickItem);

       


        OnInit();

    }


    void OnInit()
    {
        btnIcon.sprite = itemEquipmentData.equipmentIcon;
        itemEquippedName = itemEquipmentData.equipmentName;
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnClickItem()
    {
        
        
        UIUpdate();
        btnBuy.onClick.AddListener(OnClickBuy);
        btnSelect_Equipped.onClick.AddListener(OnClickSelect);

        itemOnclickEvent(idPlayerEquipmentLocation, itemEquippedName);

    }

    void OnClickSelect()
    {
        PlayerDataManager.Instance.playerEquipmentData.idListEquipment = idPlayerEquipmentLocation;
        PlayerDataManager.Instance.playerEquipmentData.equipmentName = itemEquippedName;
        PlayerDataManager.Instance.UpdatePlayerEquipmentData();
        UIUpdate();

    }

    void OnClickBuy()
    {
        GetDictionaryEquipmentDataByID();
        itemDictionaryData[itemEquippedName] = 1;
        PlayerDataManager.Instance.playerEquipmentData.UpdateDictionaryWapper(idPlayerEquipmentLocation).FromDictionary(itemDictionaryData);
        PlayerDataManager.Instance.UpdatePlayerEquipmentData();
        UIUpdate();


    }

    void UIUpdate()
    {

        GetDictionaryEquipmentDataByID();

        int equipmentPurchaseState = itemDictionaryData[itemEquippedName];
        
        if (equipmentPurchaseState == 0)
        {
            btnSelect_Equipped.gameObject.SetActive(false);
            btnBuy.gameObject.SetActive(true);
            textPrice.text = itemEquipmentData.equipmentPrice.ToString();
        }
        if (equipmentPurchaseState == 1)
        {
            btnSelect_Equipped.gameObject.SetActive(true);
            btnBuy.gameObject.SetActive(false);
            textSelect_Equipped.text = "Select";

        }
        if (PlayerDataManager.Instance.playerEquipmentData.equipmentName == itemEquippedName)
        {
            btnSelect_Equipped.gameObject.SetActive(true);
            btnBuy.gameObject.SetActive(false);
            textSelect_Equipped.text = "Equipped";
        }
    }

    void GetDictionaryEquipmentDataByID()
    {
        switch (idPlayerEquipmentLocation)
        {
            case 0:
                itemDictionaryData = PlayerDataManager.Instance.playerEquipmentData.dictionartHat.ToDictionary();
                break;
            case 1:
                itemDictionaryData = PlayerDataManager.Instance.playerEquipmentData.dictionartPant.ToDictionary();
                break;
            case 2:
                itemDictionaryData = PlayerDataManager.Instance.playerEquipmentData.dictionartShield.ToDictionary();
                break;
            case 3:
                itemDictionaryData = PlayerDataManager.Instance.playerEquipmentData.dictionartSkin.ToDictionary();
                break;
        }
    }


}
