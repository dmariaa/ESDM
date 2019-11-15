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

        private int currentSelectedChild = 0;

        // Start is called before the first frame update
        void Start()
        {
            CreateMenu();
            SelectChild(currentSelectedChild);
        }

        // Update is called once per frame
        void Update()
        {        
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                UnSelectChild(currentSelectedChild);

                do
                {
                    currentSelectedChild = (currentSelectedChild == transform.childCount - 1) ? 0 : currentSelectedChild + 1;
                } while (options[currentSelectedChild].isSpacer);
                
                SelectChild(currentSelectedChild);
            } else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                UnSelectChild(currentSelectedChild);
                do
                { 
                    currentSelectedChild = (currentSelectedChild == 0) ? transform.childCount - 1 : currentSelectedChild - 1;
                } while (options[currentSelectedChild].isSpacer);
                SelectChild(currentSelectedChild);
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                RunAction(currentSelectedChild);
            }
        }

        void RunAction(int selectedMenu)
        {
            MenuOption menuOption = options[selectedMenu];
            if(menuOption.destination != null)
            {
                menuOption.destination.transform.gameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
        }

        void SelectChild(int child)
        {
            child = transform.childCount - child - 1;
            GameObject selected = transform.GetChild(child).gameObject;
            Text selectedText = selected.GetComponent<Text>();
            selectedText.fontStyle = FontStyle.Bold;
        }

        void UnSelectChild(int child)
        {
            child = transform.childCount - child - 1;
            GameObject selected = transform.GetChild(child).gameObject;
            Text selectedText = selected.GetComponent<Text>();
            selectedText.fontStyle = FontStyle.Normal;
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
            trans.sizeDelta = new Vector2(0f, 30.0f);

            Text text = optionGameObject.AddComponent<Text>();
            text.text = label;
            text.font = this.MenuFont;
            text.material = this.MenuFontMaterial;        
            text.color = MenuTextColor;
            text.fontSize = FontSize;
            text.alignment = TextAnchor.MiddleLeft;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
        }
    }
}

