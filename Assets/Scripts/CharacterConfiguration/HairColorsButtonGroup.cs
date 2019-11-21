using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HairColorsButtonGroup : MonoBehaviour
{

    public int NumberOfColors = 64;

    private List<Color> Palette = new List<Color>();
    private GameObject buttonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonPrefab = (GameObject) Resources.Load("Prefabs/ColorToggle", typeof(GameObject));
        GeneratePalette(NumberOfColors, 0, 1, HSVRainbow);

        for (int i = 0; i < NumberOfColors; i++)
        {
            GenerateColorButton(Palette[i], i);
        }
    }

    private void GenerateColorButton(Color color, int position)
    {
        int numOfColumns = 4;
        int row = position / numOfColumns;
        int col = position % numOfColumns;
        
        GameObject colorButton = (GameObject)Instantiate(buttonPrefab, transform);
        colorButton.transform.SetParent(transform);

        /*
        RectTransform rectTransform = colorButton.GetComponent<RectTransform>();
        var sizeDelta = rectTransform.sizeDelta;
        rectTransform.anchoredPosition = new Vector2(col * sizeDelta.x, row * sizeDelta.y * -1.0f);
        rectTransform.localScale = Vector3.one;
        */

        // TOGGLE COMPONENT
        Toggle toggle = colorButton.GetComponent<Toggle>();
        toggle.isOn = false;
        toggle.group = transform.GetComponent<ToggleGroup>();

        GameObject background = toggle.targetGraphic.gameObject;
        if (background)
        {
            background.GetComponent<Image>().color = color;
        }
    }
    private void GeneratePalette(int numberOfColors, int start, int end, Func<float, float, float, Color> colorFunction)
    {
        float step = ((float) end - start) / numberOfColors;
        float x = start;
        Palette.Clear();

        for (; --numberOfColors >= 0; x += step)
        {
            Color color = colorFunction(x, 1, 1);
            Palette.Add(color);
        }
    }

    private Color HSVRainbow(float value, float s = 1.0f, float v = 1.0f)
    {
        return Color.HSVToRGB(value, s, v);
    }
}
