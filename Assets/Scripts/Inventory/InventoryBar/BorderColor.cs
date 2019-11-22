using UnityEngine.UI;
using UnityEngine;

public class BorderColor : MonoBehaviour
{
    public enum colors { ORANGE, GREEN, BLUE, YELLOW, PINK, PURPLE, BLACK };
    public colors color;
    string colorCode;
    Color oColor;
    // Start is called before the first frame update
    void Start()
    {
        //color = colors.BLACK;
        //GetComponent<Image>().color = new Color32(255, 0, 225, 100);
    }

    // Update is called once per frame
    void Update()
    {
        elegirColor();
        ColorUtility.TryParseHtmlString(colorCode, out oColor);
        GetComponent<Image>().color = oColor;
    }

    void elegirColor()
    {
        switch (color)
        {
            case colors.ORANGE:
                colorCode = "#D26D1B";
                break;

            case colors.GREEN:
                colorCode = "#B8C230";
                break;

            case colors.BLUE:
                colorCode = "#55ABE3";
                break;

            case colors.YELLOW:
                colorCode = "#F4D010";
                break;

            case colors.PINK:
                colorCode = "#CE5B92";
                break;

            case colors.PURPLE:
                colorCode = "#740A51";
                break;

            case colors.BLACK:
                colorCode = "#000000";
                break;
        }
    }
}
