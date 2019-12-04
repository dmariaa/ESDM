using ESDM.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasRenderer), typeof(Image))]
    public class InventoryPanelSlot : MonoBehaviour, IPointerClickHandler
    {
        public Vector2Int gridPosition;
            
        private AbstractItem _item;
        private Image _border;
        private Image _innerBorder;
        private Image _itemImage;
        
        public AbstractItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                
                _itemImage.sprite = _item.ItemSprite;
                Color color = Color.white;
                color.a = (_item == null) ? 0.0f : 1.0f;
                _itemImage.color = color;
            }
        }
        
        private void Start()
        {
            _border = GetComponent<Image>();
            _innerBorder = transform.GetChild(0).GetComponent<Image>();
            _itemImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            
            _border.material = new Material( Resources.Load<Material>("Shaders/ItemSlotMaterial"));
            _innerBorder.material = new Material( Resources.Load<Material>("Shaders/ItemSlotMaterial"));
            
            _border.material.SetFloat("_FlashingSpeed", 3.0f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IInventorySlotClickHandler>(transform.parent.gameObject, null,
                (handler, data) => { handler.InventorySlotClick(this); });
        }

        public void SelectSlot(bool select, bool flashing = false)
        {
            _border.color = select ? Color.white : Color.black;
            _border.material.SetFloat("_Flashing", flashing ? 1.0f : 0.0f);
        }
    }

    public interface IInventorySlotClickHandler : IEventSystemHandler
    {
        void InventorySlotClick(InventoryPanelSlot slot);
    }
}