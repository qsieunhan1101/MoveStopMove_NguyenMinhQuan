using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    [SerializeField] private Button btnContinue;
    public static Action failEvent;

    private void Awake()
    {
        btnContinue.onClick.AddListener(OnContinue);
    }

    private void OnContinue()
    {
        failEvent?.Invoke();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }
}
