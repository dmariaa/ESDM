using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemInformationPanel : MonoBehaviour
    {
        private Image _icon;
        private Text _name;
        private Text _description;

        private void Initialize()
        {
            if(_icon==null) _icon = transform.Find("Icon").GetComponent<Image>();
            if(_name==null) _name = transform.Find("Name").GetComponent<Text>();
            if(_description==null) _description = transform.Find("Description").GetComponent<Text>();
        }

        public void Show(AbstractItem item)
        {
            Initialize();
            gameObject.SetActive(true);
            _icon.sprite = item.ItemSprite;
            _name.text = item.Name;
            _description.text = item.Description;
        }

        public void Hide()
        {
            Initialize();
            Clear();
            gameObject.SetActive(false);
        }

        public void Toggle(AbstractItem item)
        {
            if (gameObject.active)
            {
                Hide();
            }
            else
            {
                Show(item);
            }
        }

        private void Clear()
        {
            _icon.sprite = null;
            _name.text = "";
            _description.text = "";
        }
    }
}