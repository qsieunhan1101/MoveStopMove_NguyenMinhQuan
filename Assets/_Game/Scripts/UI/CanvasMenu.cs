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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickPlay()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();


        GameManager.Instance.ChangeState(GameState.Gameplay);
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
