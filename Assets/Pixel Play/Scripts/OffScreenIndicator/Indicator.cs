using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    private Image indicatorImage;
    private Text distanceText;

    //extend
    [SerializeField] private Image backgroundTextImage;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;

    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    void Awake()
    {
        indicatorImage = transform.GetComponent<Image>();
        distanceText = transform.GetComponentInChildren<Text>();


    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetImageColor(Color color)
    {
        if (color == null)
        {
            color = Color.white;
        }


        indicatorImage.color = color;


        if (indicatorType == IndicatorType.ARROW)
        {
            backgroundTextImage.color = color;

        }
        if (indicatorType == IndicatorType.BOX)
        {
            nameText.color = color;
        }
    }

    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>
    public void SetDistanceText(float value)
    {
        //distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";


        //
        scoreText.text = value.ToString();

    }

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTextRotation(Quaternion rotation)
    {
        distanceText.rectTransform.rotation = rotation;

        //
        scoreText.rectTransform.rotation = rotation;

    }

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        transform.gameObject.SetActive(value);
    }





    //extend
    public void SetName(string value)
    {
        if (value == null)
        {
            value = "IM BOT";
        }

        if (indicatorType == IndicatorType.BOX)
        {
            nameText.text = value;
        }
    }
}

public enum IndicatorType
{
    BOX,
    ARROW
}
