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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClickHome()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }
    private void OnClickContinue()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
