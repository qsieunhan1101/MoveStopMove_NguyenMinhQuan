using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] private Button btnExit;

    [SerializeField] private GameObject[] Tabs;
    [SerializeField] private Image[] TabButtons;
    [SerializeField] private Sprite InactiveTabBG, ActiveTabBG;
    public Vector2 InactiveTabButtonSize, ActiveTabButtonSize;



    private void Awake()
    {
        btnExit.onClick.AddListener(OnClickExit);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToTab(int tabId)
    {
        foreach (GameObject go in Tabs)
        {
            go.SetActive(false);
        }
        Tabs[tabId].SetActive(true);

        foreach (Image im in TabButtons)
        {
            im.sprite = InactiveTabBG;
            Color color = im.color;
            color.a = 0f;
            im.color = color; 
            //im.rectTransform.sizeDelta = InactiveTabButtonSize;
        }
        //TabButtons[tabId].sprite = ActiveTabBG;
        TabButtons[tabId].color = new Color(TabButtons[tabId].color.r, TabButtons[tabId].color.g, TabButtons[tabId].color.b, 255);

        //TabButtons[tabId].rectTransform.sizeDelta = ActiveTabButtonSize;

    }
    void OnClickExit()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }
}
