using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    [SerializeField] private Button btnContinue;

    private void Awake()
    {
        btnContinue.onClick.AddListener(OnContinue);
    }

    private void OnContinue()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }
}
