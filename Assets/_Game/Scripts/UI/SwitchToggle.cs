using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleRectTransform;

    [SerializeField] private Toggle toggle;
    Vector2 handlePosition;

    [SerializeField] private Image handleImg;
    [SerializeField] private Sprite handleOnImg;
    [SerializeField] private Sprite handleOffImg;
    private void Awake()
    {
        handlePosition = uiHandleRectTransform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
            OnSwitch(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        handleImg.sprite = on ? handleOnImg : handleOffImg;
    }
}
