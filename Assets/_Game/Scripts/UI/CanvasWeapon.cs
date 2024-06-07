using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeapon : UICanvas
{
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnSelect;

    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponName;

    //
    [SerializeField] private UserData userData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private WeaponType weaponType;



    ////////
    [SerializeField] private IconData iconData;
    [SerializeField] private IconIndex iconIndex;

    
    public static Action<WeaponType> buttonSelectEvent;


    private void Awake()
    {
        btnExit.onClick.AddListener(OnClickExit);
        btnLeft.onClick.AddListener(OnClickLeft);
        btnRight.onClick.AddListener(OnClickRight);
        btnSelect.onClick.AddListener(OnClickSelect);
    }


    // Start is called before the first frame update
    void Start()
    {

        OnInit();

    }

    void OnInit()
    {
        weaponType = WeaponType.Hammer;
        SetUIDataWeapon();
    }



    void OnClickExit()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }

    void SetWeaponData()
    {
        weaponData = userData.GetWeaponData(weaponType);
    }

    void SetUIDataWeapon()
    {
        /* iconWeapon.sprite = iconData.GetIconWeaponSprite(iconIndex);
         nameWeapon.text = iconData.GetNameWeapon(iconIndex);*/

        SetWeaponData();
        weaponIcon.sprite = weaponData.weaponIcon;
        weaponName.text = weaponData.weaponName;
    }

    void OnClickLeft()
    {
        /*   iconIndex = (IconIndex)Mathf.Max((int)iconIndex - 1, 0);
           SetUIDataWeapon();*/


        weaponType = (WeaponType)Mathf.Max((int)weaponType - 1, 0);
        SetUIDataWeapon();
    }
    void OnClickRight()
    {
        /*        iconIndex = (IconIndex)Mathf.Min((int)iconIndex + 1, System.Enum.GetValues(typeof(IconIndex)).Length - 1);
                SetUIDataWeapon();*/

        weaponType = (WeaponType)Mathf.Min((int)weaponType + 1, System.Enum.GetValues(typeof(WeaponType)).Length - 1);
        SetUIDataWeapon();

    }
    private void OnClickSelect()
    {
        if (buttonSelectEvent != null)
        {

            buttonSelectEvent(weaponType);
        }
    }



}
