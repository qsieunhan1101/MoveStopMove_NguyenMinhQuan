using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGold : UICanvas
{
    [SerializeField] private TextMeshProUGUI textGold;

    private bool isStart = false;
    private void Start()
    {
        UIUpdate();
        isStart = true;
    }
    private void OnEnable()
    {
        if (isStart == true)
        {
            UIUpdate();
        }
        CanvasWeapon.buttonBuyEvent += UIUpdate;
        SkinItemUI.buttonBuyEvent += UIUpdate;
    }
    private void OnDisable()
    {
        CanvasWeapon.buttonBuyEvent -= UIUpdate;
        SkinItemUI.buttonBuyEvent -= UIUpdate;
    }
    public void UIUpdate()
    {
        int gold = PlayerDataManager.Instance.playerWeaponData.golds;
        textGold.text = gold.ToString();
    }
}
