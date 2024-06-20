using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] private Button btnContinue;

    public static Action victoryEvent;

    private void Awake()
    {
        btnContinue.onClick.AddListener(OnContinue);
    }
    private void OnContinue()
    {
        victoryEvent?.Invoke();
        LevelManager.Instance.Level++;
        LevelManager.Instance.SaveLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }
}
