using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ESDM.MenuSystem
{
    public class Menu : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
    {
        public Vector2 InitialPosition = new Vector2(10.0f, 10.0f);
        public Font MenuFont;
        public int FontSize = 30;
        public Material MenuFontMaterial;
        public Color MenuTextColor = Color.white;
        public Sprite SelectedMenuSprite;
        public List<MenuOption> options;

        private GameObject selectedMenuGameObject;
        private string currentSelectedChild;

        private Dictionary<string, MenuOption> menuOptions;

        // Start is called before the first frame update
        void Start()
        {
            menuOptions = new Dictionary<string, MenuOption>();
            CreateMenuButton();
            CreateMenu();
            SelectChild(options[0].name);
        }

        void CreateMenuButton()
        {
            selectedMenuGameObject = new GameObject("MenuButton");
            selectedMenuGameObject.transform.localScale = Vector3.one;
            selectedMenuGameObject.transform.localPosition = new Vector3(-25.0f, 0.0f, 0.0f);
            
            RectTransform rectTransform = selectedMenuGameObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.0f, 0.5f);
            rectTransform.sizeDelta = new Vector2(20.0f, 20.0f);
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = new Vector3(-25.0f, 0.0f, 0.0f);

            Image image = selectedMenuGameObject.AddComponent<Image>();
            image.sprite = SelectedMenuSprite;
        }
        
        void CreateMenu()
        {
            menuOptions.Clear();
            
            Vector3 position = InitialPosition;
            position.x += 50.0f;
        
            for (int i = options.Count - 1; i >= 0; i--)
            {
                MenuOption option = options[i];
                if (!option.isSpacer)
                {
                    option.GameObject = CreateMenuOption(option.name, option.label, position);
                    menuOptions.Add(option.name, option);
                }
                position.y += 50.0f;
            }
        }

        GameObject CreateMenuOption(string name, string label, Vector3 position)
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
            text.alignment = TextAnchor.MiddleLeft;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;

            trans.sizeDelta = new Vector2(text.preferredWidth, 30.0f);

            return optionGameObject;
        }
        
        void SelectChild(string child)
        {
            MenuOption menuOption = menuOptions[child];
            
            if(menuOption != null){
                Text selectedText = menuOption.GameObject.GetComponent<Text>();
                selectedText.fontStyle = FontStyle.Bold;

                RectTransform rectTransform = selectedMenuGameObject.GetComponent<RectTransform>();
                rectTransform.SetParent(menuOption.GameObject.transform, false);
                rectTransform.localPosition = new Vector3(-25.0f, 15.0f, 0.0f);
                rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.0f, 0.5f);
                rectTransform.sizeDelta = new Vector2(20.0f, 20.0f);
                rectTransform.localScale = Vector3.one;

                currentSelectedChild = menuOption.name;
            }
        }

        void UnSelectChild(string child)
        {
            MenuOption menuOption = menuOptions[child];
            
            if (menuOption != null)
            {
                Text selectedText = menuOption.GameObject.GetComponent<Text>();
                selectedText.fontStyle = FontStyle.Normal;
            }
        }
        
        private MenuOption GetPreviousOption(string option)
        {
            for (int i = 0, length = menuOptions.Count; i < length; i++)
            {
                if (menuOptions.ElementAt(i).Value.name == option)
                {
                    if (i == length - 1)
                    {
                        return menuOptions.ElementAt(0).Value;
                    }

                    return menuOptions.ElementAt(i + 1).Value;
                }
            }

            return null;
        }

        private MenuOption GetNextOption(string option)
        {
            for (int i = menuOptions.Count - 1; i >= 0; i--)
            {
                if (menuOptions.ElementAt(i).Value.name == option)
                {
                    if (i == 0)
                    {
                        return menuOptions.ElementAt(menuOptions.Count - 1).Value;
                    }

                    return menuOptions.ElementAt(i - 1).Value;
                }                
            }

            return null;
        }

        void RunAction(string optionName)
        {
            MenuOption menuOption = menuOptions[optionName];
            
            if(menuOption.destination != null)
            {
                menuOption.destination.transform.gameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
            
            ExecuteEvents.Execute<IMenuEventHandler>(transform.gameObject, null,
                (handler, eventData) => { handler.MenuSelected(menuOption.name); } );
        }
        
        // Update is called once per frame
        void Update()
        {        
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                UnSelectChild(currentSelectedChild);
                SelectChild(GetNextOption(currentSelectedChild).name);
            } else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                UnSelectChild(currentSelectedChild);
                SelectChild(GetPreviousOption(currentSelectedChild).name);
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                RunAction(currentSelectedChild);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RunAction(eventData.pointerEnter.name);            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UnSelectChild(currentSelectedChild);
            SelectChild(menuOptions[eventData.pointerEnter.name].name);
        }
    }
}

