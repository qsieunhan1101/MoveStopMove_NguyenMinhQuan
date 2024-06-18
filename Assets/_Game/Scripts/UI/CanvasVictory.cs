using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] private Button btnContinue;

    private void Awake()
    {
        btnContinue.onClick.AddListener(OnContinue);
    }
    private void OnContinue()
    {
        LevelManager.Instance.Level++;
        LevelManager.Instance.SaveLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }
}
