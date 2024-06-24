using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMenu : UICanvas
{

    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnWeapon;
    [SerializeField] private Button btnSkin;
    private void Awake()
    {
        btnPlay.onClick.AddListener(OnClickPlay);
        btnWeapon.onClick.AddListener(OnClickWeapon);
        btnSkin.onClick.AddListener(OnClickSkin);
    }
    private void OnClickPlay()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        CameraFollow.Instance.MoveCamera(new Vector3(0,10,-20) , 0.2f);
    }
    private void OnClickWeapon()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasWeapon>();
    }
    private void OnClickSkin()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasShop>();
    }
}
