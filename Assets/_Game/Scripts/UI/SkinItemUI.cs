using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinItemUI : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button btnItem;
    public Button btnSelect_Equipped;
    public Button btnBuy;


    [Header("Image")]
    [SerializeField] private Image btnIcon;

    [Header("Sprite")]
    [SerializeField] private Sprite selectedSprite;
    public GameObject selectedIcon;

    [Header("Text")]
    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI textEquipped;

    [Header("Data")]
    private Dictionary<string, int> itemDictionaryData = new Dictionary<string, int>();
    public string itemEquippedName;
    public int idPlayerEquipmentLocation = 0;
    public EquipmentData itemEquipmentData;
    public static Action<int, string> itemOnclickEvent;

    public static Action buttonBuyEvent;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        btnItem.onClick.AddListener(OnClickItem);
    }


    private void OnInit()
    {
        btnIcon.sprite = itemEquipmentData.equipmentIcon;
        //itemEquippedName = itemEquipmentData.equipmentName;
    }

    private void OnClickItem()
    {
        Debug.Log("OnClickItem");
        UIUpdate();
        btnBuy.onClick.RemoveAllListeners();
        btnSelect_Equipped.onClick.RemoveAllListeners();

        SoundManager.Instance.SpawnAndPlaySound(SoundType.ButtonSound);

        btnBuy.onClick.AddListener(OnClickBuy);
        btnSelect_Equipped.onClick.AddListener(OnClickSelect);

        if (itemOnclickEvent != null)
        {

            itemOnclickEvent(idPlayerEquipmentLocation, itemEquippedName);
        }

    }

    private void OnClickSelect()
    {
        PlayerEquipmentData p = PlayerDataManager.Instance.playerEquipmentData;
        p.idListEquipment = idPlayerEquipmentLocation;
        p.equipmentName = itemEquippedName;
        PlayerDataManager.Instance.UpdatePlayerEquipmentData();
        UIUpdate();

    }

    private void OnClickBuy()
    {
        GetDictionaryEquipmentDataByID();
        //


        if (PlayerDataManager.Instance.playerWeaponData.golds < itemEquipmentData.equipmentPrice)
        {
            Debug.Log("khong du tien");
        }
        else
        {
            itemDictionaryData[itemEquippedName] = 1;
            PlayerDataManager.Instance.playerEquipmentData.UpdateDictionaryWapper(idPlayerEquipmentLocation).FromDictionary(itemDictionaryData);
            PlayerDataManager.Instance.UpdatePlayerEquipmentData();
            PlayerWeaponData p = PlayerDataManager.Instance.playerWeaponData;
            p.golds = p.golds - itemEquipmentData.equipmentPrice;
            PlayerDataManager.Instance.UpdateDataPlayer();

        }

        UIUpdate();
        buttonBuyEvent?.Invoke();
    }

    private void UIUpdate()
    {
        Debug.Log("UIUpdate");
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
            //textSelect_Equipped.text = "Select";

        }
        if (PlayerDataManager.Instance.playerEquipmentData.equipmentName == itemEquippedName)
        {
            btnSelect_Equipped.gameObject.SetActive(false);
            btnBuy.gameObject.SetActive(false);
            textEquipped.gameObject.SetActive(true);
        }
    }

    private void GetDictionaryEquipmentDataByID()
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


    public string GetNameItem()
    {
        return itemEquippedName;
    }

    public void OnClickSelf()
    {
        btnItem.onClick.Invoke();
        Debug.Log("Tu bam chinh minh");


    }
}
