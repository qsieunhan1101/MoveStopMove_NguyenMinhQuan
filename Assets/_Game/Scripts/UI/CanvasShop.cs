using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] private Button btnExit;

    [SerializeField] private GameObject[] Tabs;
    [SerializeField] private Image[] TabButtons;
    [SerializeField] private Button[] Buttons;
    [SerializeField] private Sprite InactiveTabBG, ActiveTabBG;

    private int idPlayerEquipmentLocation;
    private string itemEquippedName;

    public Vector2 InactiveTabButtonSize, ActiveTabButtonSize;

    public static Action<int, string> exitShopEvent;


    private void Awake()
    {
        btnExit.onClick.AddListener(OnClickExit);
    }

    private void OnEnable()
    {
        TapEuippedUI();
    }

    public void SwitchToTab(int tabId)
    {

        foreach (GameObject go in Tabs)
        {
            go.SetActive(false);
        }
        Tabs[tabId].SetActive(true);

        foreach (Image im in TabButtons)
        {
            im.sprite = InactiveTabBG;
            Color color = im.color;
            color.a = 0f;
            im.color = color; 
            //im.rectTransform.sizeDelta = InactiveTabButtonSize;
        }
        //TabButtons[tabId].sprite = ActiveTabBG;
        TabButtons[tabId].color = new Color(TabButtons[tabId].color.r, TabButtons[tabId].color.g, TabButtons[tabId].color.b, 255);

        //TabButtons[tabId].rectTransform.sizeDelta = ActiveTabButtonSize;

    }
    private void OnClickExit()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
        PlayerEquipmentData p = PlayerDataManager.Instance.playerEquipmentData;
        idPlayerEquipmentLocation = p.idListEquipment;
        itemEquippedName = p.equipmentName;
        if (exitShopEvent != null)
        {
            exitShopEvent(idPlayerEquipmentLocation, itemEquippedName);
        }
    }

    private void TapEuippedUI()
    {
        int idTapEquipped = PlayerDataManager.Instance.playerEquipmentData.idListEquipment;
        Buttons[idTapEquipped].onClick.Invoke();
    }
}
