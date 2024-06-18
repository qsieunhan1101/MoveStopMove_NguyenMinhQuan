using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeapon : UICanvas
{
    [Header ("Button")]
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnBuy;

    [Header("Image/Sprite")]
    [SerializeField] private Image weaponImg;
    [SerializeField] private Sprite btnSelectImg;
    [SerializeField] private Sprite btnEquippedImg;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI btnSelectText;
    [SerializeField] private TextMeshProUGUI btnBuyText;

    [Header ("Data") ]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private WeaponType weaponType;

   

    
    public static Action<WeaponType> buttonSelectEvent;



    private void Awake()
    {
        btnExit.onClick.AddListener(OnClickExit);
        btnLeft.onClick.AddListener(OnClickLeft);
        btnRight.onClick.AddListener(OnClickRight);
        btnSelect.onClick.AddListener(OnClickSelect);
        btnBuy.onClick.AddListener(OnClickBuy);
    }


    // Start is called before the first frame update
    void Start()
    { 
        OnInit();

    }
    private void OnEnable()
    {
        WeaponEquippedUI();
    }

    void OnInit()
    {
        //weaponType = WeaponType.Hammer;
        weaponType = PlayerDataManager.Instance.GetWeaponState();
        UIUpdate();
    }


    void OnClickExit()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }

    void GetWeaponData()
    {
        weaponData = LocalDataManager.Instance.UserData.GetWeaponData(weaponType);
    }

    void UIUpdate()
    {
        GetWeaponData();

        if (PlayerDataManager.Instance.playerWeaponData.weaponPurchaseState[this.weaponType] == 0)
        {
            btnSelect.gameObject.SetActive(false);
            btnBuy.gameObject.SetActive(true);

        }
        if (PlayerDataManager.Instance.playerWeaponData.weaponPurchaseState[this.weaponType] == 1)
        {
            btnSelect.gameObject.SetActive(true);
            btnSelect.image.sprite = btnSelectImg;
            btnSelectText.text = "Select";
            btnBuy.gameObject.SetActive(false);
        }
        if (PlayerDataManager.Instance.playerWeaponData.weaponPurchaseState[this.weaponType] == 2)
        {
            btnSelect.gameObject.SetActive(true);
            btnSelect.image.sprite = btnEquippedImg;
            btnSelectText.text = "Eqquiped";
            btnBuy.gameObject.SetActive(false);
        }


        weaponImg.sprite = weaponData.weaponIcon;
        weaponName.text = weaponData.weaponName;
        btnBuyText.text = weaponData.weaponPrice.ToString();
    }

    void OnClickLeft()
    {
        weaponType = (WeaponType)Mathf.Max((int)weaponType - 1, 0);
        UIUpdate();
    }
    void OnClickRight()
    {
        weaponType = (WeaponType)Mathf.Min((int)weaponType + 1, System.Enum.GetValues(typeof(WeaponType)).Length - 1);
        UIUpdate();

    }
    private void OnClickSelect()
    {
        if (buttonSelectEvent != null)
        {

            buttonSelectEvent(weaponType);
        }
        PlayerWeaponData p = PlayerDataManager.Instance.playerWeaponData;
        p.weaponPurchaseState[this.weaponType] = 2;

        for (int i=0; i< p.weaponPurchaseState.Count-1;i++)
        {
            if (p.weaponPurchaseState[(WeaponType)i] == 2 && p.weaponPurchaseState.Keys.ElementAt(i) != this.weaponType)
            {
                p.weaponPurchaseState[(WeaponType)i] = 1;
            }
        }

        PlayerDataManager.Instance.UpdateDataPlayer();
        UIUpdate();
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }

    private void OnClickBuy()
    {
        if (PlayerDataManager.Instance.playerWeaponData.golds < weaponData.weaponPrice)
        {
            Debug.Log("khong du tien");

        }
        else
        {
            Debug.Log("da mua");
 
            PlayerWeaponData p = PlayerDataManager.Instance.playerWeaponData;

            p.golds = p.golds - weaponData.weaponPrice;
            p.weaponPurchaseState[this.weaponType] = 1;

            PlayerDataManager.Instance.UpdateDataPlayer();

            UIUpdate();

        }
    }

    private void WeaponEquippedUI()
    {
        weaponType = PlayerDataManager.Instance.GetWeaponState();
        UIUpdate();
    }


}
