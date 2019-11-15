using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ESDM.MenuSystem
{
    public class Menu : MonoBehaviour
    {
        public Vector2 InitialPosition = new Vector2(10.0f, 10.0f);
        public Font MenuFont;
        public int FontSize = 30;
        public Material MenuFontMaterial;
        public Color MenuTextColor = Color.white;
        public List<MenuOption> options;

        // Start is called before the first frame update
        void Start()
        {
            CreateMenu();
        }

        // Update is called once per frame
        void Update()
        {        
        }

        void CreateMenu()
        {
            Vector3 position = InitialPosition;
        
            for (int i = options.Count - 1; i >= 0; i--)
            {
                MenuOption option = options[i];
                CreateMenuOption(option.name, option.label, position);
                position.y += 50.0f;
            }
        }

        void CreateMenuOption(string name, string label, Vector3 position)
        {
            GameObject optionGameObject = new GameObject(name);
            optionGameObject.transform.SetParent(transform);

            RectTransform trans = optionGameObject.AddComponent<RectTransform>();
            trans.anchorMin = trans.anchorMax = Vector2.zero;
            trans.pivot = Vector2.zero;
            trans.localPosition = position;
            trans.localScale = Vector3.one;

            Text text = optionGameObject.AddComponent<Text>();
            text.text = label;
            text.font = this.MenuFont;
            text.material = this.MenuFontMaterial;        
            text.color = MenuTextColor;
            text.fontSize = FontSize;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
        }
    }
}

