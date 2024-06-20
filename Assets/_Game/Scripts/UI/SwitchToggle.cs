using UnityEngine;
using UnityEngine.UI;

public enum ToggleType
{
    Sound = 0,
    Vibration = 1,
}
public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private ToggleType toggleType;
    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image handleImg;
    [SerializeField] private Sprite handleOnImg;
    [SerializeField] private Sprite handleOffImg;
    private Vector2 handlePosition;


    private void Awake()
    {
        handlePosition = uiHandleRectTransform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        handleImg.sprite = on ? handleOnImg : handleOffImg;

        if (toggleType == ToggleType.Sound)
        {
            if (on)
            {
                SoundManager.Instance.UnMuteAllAudio();
            }
            else if (!on)
            {
                SoundManager.Instance.MuteAllAudio();
            }
        }


    }
}
