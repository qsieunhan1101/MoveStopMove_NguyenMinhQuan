using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Button btnPause;

    private void Awake()
    {
        btnPause.onClick.AddListener(OnClickPause);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClickPause()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasPause>();
    }
}
