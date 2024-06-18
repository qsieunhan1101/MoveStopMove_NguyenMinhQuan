using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGold : UICanvas
{
    [SerializeField] private TextMeshProUGUI textGold;
    private void Start()
    {
        UIUpdate();
    }
    void UIUpdate()
    {
        int gold = PlayerDataManager.Instance.playerWeaponData.golds;
        textGold.text = gold.ToString();
    }
}
