using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.InventoryBar
{
    public class PanelSlider : MonoBehaviour
    {
        public float SecondsToOpen = 0.65f;
    
        private Transform _child;
        private Vector2 _childSize;
        private RectTransform _rectTransform;
        private int _direction = 0;
        private float _speed = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            _child = transform.GetChild(0);
            if (_child)
            {
                _childSize = _child.GetComponent<RectTransform>().sizeDelta;
                _rectTransform = GetComponent<RectTransform>();
                _rectTransform.sizeDelta = new Vector2(0, _childSize.y);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_child == null) return;
        
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_direction == -1 || _rectTransform.sizeDelta.x == 0)
                {
                    _speed = _childSize.x / SecondsToOpen;
                    _direction = 1;
                } else if (_direction == 1 || _rectTransform.sizeDelta.x == _childSize.x)
                {
                    _direction = -1;
                    _speed = _childSize.x / SecondsToOpen;
                }
            }
        
            if (_direction != 0)
            {
                _rectTransform.sizeDelta =  new Vector2(
                    _rectTransform.sizeDelta.x + Time.deltaTime * _speed * (float)_direction , 
                    _childSize.y);

                if (_direction==1)
                {
                    if (_rectTransform.sizeDelta.x >= _childSize.x) 
                    {
                        _rectTransform.sizeDelta = new Vector2(_childSize.x, _childSize.y);
                        _direction = 0;
                        
                        foreach (Inventory target in FindObjectsOfType<Inventory>())
                        {
                            ExecuteEvents.Execute<IPanelSliderEventHandler>(target.gameObject, null,
                                (handler, data) => { handler.PanelSlide(true); });
                        }
                    }
                }
                else if (_direction==-1)
                {
                    if (_rectTransform.sizeDelta.x <= 0) 
                    {
                        _rectTransform.sizeDelta = new Vector2(0, _childSize.y);
                        _direction = 0;
                        
                        foreach (Inventory target in FindObjectsOfType<Inventory>())
                        {
                            ExecuteEvents.Execute<IPanelSliderEventHandler>(target.gameObject, null,
                                (handler, data) => { handler.PanelSlide(false); });
                        }
                    }
                }
            }
        }
    }

    public interface IPanelSliderEventHandler : IEventSystemHandler
    {
        void PanelSlide(bool open);
    }
}


