using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPause : UICanvas
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnContinue;

    private void Awake()
    {
        btnHome.onClick.AddListener(OnClickHome);
        btnContinue.onClick.AddListener(OnClickContinue);
    }
    private void OnClickHome()
    {

        GameManager.Instance.ChangeState(GameState.MainMenu);
    }
    private void OnClickContinue()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }
}
