using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maua
{
    public class MauaPetal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Vector3 DirectionVector;
        public float Speed;
        public float Distance;

        private float _direction;
        private Vector3 _initialPosition;
        private Vector3 _endPosition;
        private bool _moving = false;
        
        private void Start()
        {
            _initialPosition = transform.localPosition;
            _endPosition = _initialPosition + (Distance * DirectionVector);
        }

        private void Update()
        {
            if (_moving)
            {
                transform.localPosition += Time.deltaTime * Speed * _direction * DirectionVector;

                if (_direction > 0.0f)
                {
                    if ((transform.localPosition - _endPosition).sqrMagnitude <= 0.5f)
                    {
                        transform.localPosition = _endPosition;
                        _moving = false;
                    }
                } else if (_direction < 0.0f)
                {
                    if((transform.localPosition - _initialPosition).sqrMagnitude <= 0.5f)
                    {
                        transform.localPosition = _initialPosition;
                        _moving = false;
                    }
                }
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
    }
}