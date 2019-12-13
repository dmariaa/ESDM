using System;
using ESDM.ScriptableObjects;
using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maua
{
    public class MauaPetal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public Vector3 DirectionVector;
        public KeyCode key;
        public float Speed;
        public float Distance;
        public float SecondsToOpen = 0.65f;

        private float _direction;
        private float _time = 0.0f;
        
        private Vector3 _initialPosition;
        private Vector3 _endPosition;
        private bool _moving = false;

        private int _itemIndex;

        public int ItemIndex
        {
            get => _itemIndex;
            set
            {
                _itemIndex = value;
                
                if (value > 0)
                {
                    AbstractItem item = GlobalGameState.Instance.CurrentGameState.inventory.GetItem(_itemIndex - 1);
                    SetPetalImage(item?.ItemSprite);
                }
                else
                {
                    SetPetalImage(null);
                }
            }
        }

        private void Start()
        {
            _initialPosition = transform.localPosition;
            _endPosition = _initialPosition + (Distance * DirectionVector.normalized);
        }

        private void Update()
        {
            _time = Mathf.Clamp(_time + Time.deltaTime * _direction, 0, SecondsToOpen);
            transform.localPosition = Vector3.Lerp(_initialPosition, _endPosition, _time / SecondsToOpen);
            
            if (Input.GetKeyDown(key))
            {
                ExecuteEvents.Execute<IPetalPointerEventHandler>(transform.parent.gameObject, null,
                    (handler, data) => { handler.PetalSelect(this.name); } );
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IPetalPointerEventHandler>(transform.parent.gameObject, null,
                (handler, data) => { handler.PetalEnter(this.name); } );
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IPetalPointerEventHandler>(transform.parent.gameObject, null,
                (handler, data) => { handler.PetalExit(this.name); } );
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IPetalPointerEventHandler>(transform.parent.gameObject, null,
                (handler, data) => { handler.PetalSelect(this.name); } );
        }
        
        public void Show()
        {
            _direction = 1.0f;
            _moving = true;
        }

        public void Hide()
        {
            _direction = -1.0f;
            _moving = true;
        }

        public void SetPetalImage(Sprite image)
        {
            Transform item = transform.Find("Item");
            Image itemImage = item.GetComponent<Image>();
            itemImage.sprite = image;
            Color color = Color.white;
            color.a = image == null ? 0.0f : 1.0f;
            itemImage.color = color;
        }

        public int GetIndex()
        {
            return Int32.Parse(this.name.Substring(7)) - 1;
        }
    }
}