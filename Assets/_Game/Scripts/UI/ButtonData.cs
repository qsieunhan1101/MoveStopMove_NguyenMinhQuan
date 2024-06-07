using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    [SerializeField] private IconData iconData;
    [SerializeField] private Image btnIcon;
    [SerializeField] private IconIndex iconIndex;

    [SerializeField] private ButtonType buttonType;


    // Start is called before the first frame update
    void Start()
    {
        SetTypeButton();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            btnIcon.sprite = iconData.GetIconHatSprite(iconIndex);

        }
    }

    void SetTypeButton()
    {
        switch (buttonType)
        {
            case ButtonType.Hat:
                btnIcon.sprite = iconData.GetIconHatSprite(iconIndex);
                break;
            case ButtonType.Pant:
                btnIcon.sprite = iconData.GetIconPantSprite(iconIndex);
                break;
            case ButtonType.Shield:
                btnIcon.sprite = iconData.GetIconShieldSprite(iconIndex);
                break;
            case ButtonType.Skin:
                btnIcon.sprite = iconData.GetIconSkinSprite(iconIndex);
                break;
        }
    }
}
