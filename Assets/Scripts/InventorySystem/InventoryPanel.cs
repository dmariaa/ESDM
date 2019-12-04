using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using ESDM.Utilities;
using Maua;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

namespace InventorySystem
{
    public class InventoryPanel : MonoBehaviour, IInventorySlotClickHandler, IInventorySystemMessageHandler, IMauaEventHandler
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
                int row = i % 2;
                int col = i / 2;
                
                GameObject slot = Instantiate(prefab, transform);
                slot.name = String.Format("InventorySlot[{0}][{1}]", row, col);
                slot.GetComponent<InventoryPanelSlot>().gridPosition = new Vector2Int(row, col);
                slots.Add(slot.GetComponent<InventoryPanelSlot>());
            }

            _rectTransform = GetComponent<RectTransform>();
        }

        public void InventorySlotClick(InventoryPanelSlot slot)
        {
            SelectSlot(slot);
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
            closing = false;
        }

        public void ClosePanel()
        {
            _direction = -1.0f;
            closing = true;
        }

        public void TogglePanel()
        {
            _direction *= -1.0f;
        }
        
        // TODO: REVIEW
        private bool closing = false;
        
        private void Update()
        {
            _time = Mathf.Clamp(_time + Time.deltaTime * _direction, 0.0f, SecondsToOpen);

            _rectTransform.sizeDelta = new Vector2(
                Mathf.Lerp(_closedSize, _openedSize, _time / SecondsToOpen), 
                _rectTransform.sizeDelta.y);

            if(IsOpened())
            {
                if(!closing)
                {
                    List<IInventoryPanelEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IInventoryPanelEventHandler>();
                    ExecuteEventHelper.BroadcastEvent(handlers, (handler, eventData) => { handler.InventoryPanelOpened(); });
                    closing = true;
                }

                Vector2Int slotPosition = _selectedSlot != null ? _selectedSlot.gridPosition : Vector2Int.zero;
                
                if (Input.GetKeyDown(KeyCode.W))
                {
                    slotPosition.x = Math.Min(Math.Max(slotPosition.x - 1, 0), 1);
                } 
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    slotPosition.x = Math.Min(Math.Max(slotPosition.x + 1, 0), 1);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    slotPosition.y = Math.Min(Math.Max(slotPosition.y - 1, 0), 7);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    slotPosition.y = Math.Min(Math.Max(slotPosition.y + 1, 0), 7);
                } 
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                }

                SelectSlot(transform.Find(String.Format("InventorySlot[{0}][{1}]", slotPosition.x, slotPosition.y)
                    ).GetComponent<InventoryPanelSlot>()
                );
            } 
            else if (closing && IsClosed())
            {
                
                informationPanel?.GetComponent<ItemInformationPanel>().Hide();
                
                List<IInventoryPanelEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IInventoryPanelEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, (handler, eventData) => { handler.InventoryPanelClosed(); });

                closing = false;
            }
        }

        public void SelectSlot(InventoryPanelSlot slot)
        {
            if (slot == null) return;
            
            _selectedSlot?.SelectSlot(false);
            _selectedSlot = slot;
            _selectedSlot.SelectSlot(true);
            
            if(_selectedSlot.Item != null)
            {
                informationPanel?.GetComponent<ItemInformationPanel>().Show(_selectedSlot.Item);
            }
            else
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

        public void PetalToggle(MauaPetal petal, bool open)
        {
            if (IsOpened() && _selectedSlot != null)
            {
                petal.ItemIndex = 0;
            }
        }
    }

    public interface IInventoryPanelEventHandler : IEventSystemHandler
    {
        void InventoryPanelOpened();
        void InventoryPanelClosed();
    }
}