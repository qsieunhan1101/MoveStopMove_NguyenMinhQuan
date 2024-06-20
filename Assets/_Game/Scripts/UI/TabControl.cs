using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabControl : MonoBehaviour
{

    [SerializeField] private TabType tabType;

    [Header("Button")]
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnBuy;

    [SerializeField] private SkinItemUI btnItemSelected;

    [Header("ButtonItem")]
    [SerializeField] private GameObject buttonPrefabs;
    [SerializeField] private Transform buttonParent;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textSelect_Equipped;

    private int idPlayerEquipmentLocation;




    [SerializeField] private List<EquipmentData> equipments;

    [SerializeField] private List<SkinItemUI> buttonChilds;


    // Start is called before the first frame update
    void Start()
    {
        GenerateItem();

        Debug.Log(buttonChilds.Count);


        StartCoroutine(CallFunctionAfterFrames(1, ButtonChildEquipped));

    }



    private void OnEnable()
    {
        ButtonChildEquipped();
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
            Button button = btn.GetComponent<Button>();
            SkinItemUI itemData = btn.GetComponent<SkinItemUI>();
            //set data cho button con
            itemData.btnBuy = this.btnBuy;
            itemData.btnSelect_Equipped = this.btnSelect;
            itemData.textPrice = this.textPrice;
            itemData.textEquipped = this.textSelect_Equipped;
            itemData.itemEquippedName = this.equipments[i].equipmentName;
            itemData.itemEquipmentData = this.equipments[i];

            itemData.idPlayerEquipmentLocation = this.idPlayerEquipmentLocation;
            buttonChilds.Add(itemData);
            button.onClick.AddListener(() => ItemSelectedUI(itemData));
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


    private void ButtonChildEquipped()
    {
        CheckTabType();
        string equipped = PlayerDataManager.Instance.playerEquipmentData.equipmentName;

        SkinItemUI btn;
        for (int i = 0; i < buttonChilds.Count - 1; i++)
        {
            btn = buttonChilds[i];
            if (btn.GetNameItem() == equipped)
            {

                btn.OnClickSelf();
                return;
            }
            else
            {
                buttonChilds[0].OnClickSelf();
                break;
            }
        }
    }


    private void ItemSelectedUI(SkinItemUI itemUI)
    {
        if (btnItemSelected != null)
        {
            btnItemSelected.selectedIcon.SetActive(false);
        }

        btnItemSelected = itemUI;

        btnItemSelected.selectedIcon.SetActive(true);
    }

    private IEnumerator CallFunctionAfterFrames(int frames, Action functionToCall)
    {
        for (int i = 0; i < frames; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        functionToCall?.Invoke();
    }

}
