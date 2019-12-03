using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryPanel : MonoBehaviour, IInventorySlotClickHandler, IInventorySystemMessageHandler
    {
        public GameObject informationPanel;
        public float SecondsToOpen = 0.65f;
        
        private List<InventoryPanelSlot> slots = new List<InventoryPanelSlot>();
        private InventoryPanelSlot _selectedSlot;

        private RectTransform _rectTransform;
        private float _direction = -1.0f;
        private float _closedSize = 10.0f;
        private float _openedSize = 1000.0f;
        private float _time = 0.0f;
        
        private void Start()
        {
            Inventory.Instance.RegisterListener(this);

            GameObject prefab = Resources.Load<GameObject>("Prefabs/InventorySlot");
            int numberOfSlots = Inventory.Instance.inventory.Length;

            for (int i = 0; i < numberOfSlots; i++)
            {
                GameObject slot = Instantiate(prefab, transform);
                slots.Add(slot.GetComponent<InventoryPanelSlot>());
            }

            _rectTransform = GetComponent<RectTransform>();
        }

        public void InventorySlotClick(InventoryPanelSlot slot)
        {
            if (_selectedSlot != null)
            {
                _selectedSlot.SelectSlot(false);
            }
            
            slot.SelectSlot(true);
            
            if(slot.Item != null)
            {
                informationPanel?.GetComponent<ItemInformationPanel>().Show(slot.Item);
            }
            else
            {
                informationPanel?.GetComponent<ItemInformationPanel>().Hide();
            }
            
            _selectedSlot = slot;
        }

        public void ItemAdded(int index)
        {
            slots[index].Item = Inventory.Instance.GetItem(index);
        }

        public void ItemRemoved(int index)
        {
            slots[index].Item = null;
        }
        
        public void OpenPanel()
        {
            _direction = 1.0f;
        }

        public void ClosePanel()
        {
            _direction = -1.0f;
        }

        public void TogglePanel()
        {
            _direction *= -1.0f;
        }
        
        private void Update()
        {
            _time = Mathf.Clamp(_time + Time.deltaTime * _direction, 0.0f, SecondsToOpen);

            _rectTransform.sizeDelta = new Vector2(
                Mathf.Lerp(_closedSize, _openedSize, _time / SecondsToOpen), 
                _rectTransform.sizeDelta.y);

            if (_time <= 0.0f)
            {
                informationPanel?.GetComponent<ItemInformationPanel>().Hide();
            }
        }

        public bool IsOpened()
        {
            return _time >= SecondsToOpen;
        }

        public bool IsClosed()
        {
            return _time <= 0.0f;
        }

        public bool IsOpening()
        {
            return !IsClosed() && !IsOpened();
        }
    }
}