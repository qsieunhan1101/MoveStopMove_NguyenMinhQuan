using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Button btnPause;
    [SerializeField] private TextMeshProUGUI textTotalEnemy;

    private void Awake()
    {
        btnPause.onClick.AddListener(OnClickPause);
    }

    public override void SetUp()
    {
        base.SetUp();
        UIUpdate();

    }

    private void OnEnable()
    {
        EnemyManager.totalEnemyUpdateEvent += UIUpdate;
    }
    private void OnDisable()
    {
        EnemyManager.totalEnemyUpdateEvent -= UIUpdate;

    }
    private void OnClickPause()
    {

        GameManager.Instance.ChangeState(GameState.Pause);
    }
    private void UIUpdate()
    {
        textTotalEnemy.text = EnemyManager.Instance.TotalEnemy.ToString();
    }
}
