using System.Collections.Generic;
using ESDM.ScriptableObjects;
using ESDM.Utilities;
using UnityEngine;

namespace InventorySystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteInEditMode]
    public class ItemGameObject : MonoBehaviour
    {
        [SerializeField]
        private AbstractItem _Item;
        
        public AbstractItem Item
        {
            get => _Item;
            set
            {
                _Item = value;
                GetComponent<SpriteRenderer>().sprite = _Item.ItemSprite;
            }
        }

        private SpriteRenderer _spriteRenderer;

        private bool _highlighted = false;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sharedMaterial = new Material(Resources.Load<Material>("Shaders/ItemMaterial"));
            _spriteRenderer.sharedMaterial.SetFloat("_OutlineSize", 3.0f);
            _spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            
            // TODO: ENHANCE
            if (Application.isPlaying)
            {
                if (GlobalGameState.Instance.CurrentGameState.keyPicked)
                {
                    this.gameObject.SetActive(false);
                }
                else
                {
                    this.gameObject.SetActive(true);
                }
            }
            else
            {
                this.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            // Only way to see it in editor
            if (_Item != null && _spriteRenderer.sprite != _Item.ItemSprite)
            {
                _spriteRenderer.sprite = _Item.ItemSprite;
            }

            if (_highlighted && Input.GetKeyDown(KeyCode.Space))
            {
                List<IItemEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemPickup(this); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemPickup(this); }, true);
            }
        }
        
        public void ActivateOutline(bool active, bool animated = false, int speed = 0)
        {
            Material material = GetComponent<SpriteRenderer>().material;
            material.SetFloat("_Outline", active ? 1.0f : 0.0f);
            material.SetFloat("_OutlineAnimated", animated ? 1.0f : 0.0f);
            material.SetFloat("_OutlineSpeed", speed);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.name == "Stella")
            {
                _highlighted = true;
                ActivateOutline(true);
                
                List<IItemEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemEnter(this); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemEnter(this); }, true);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.name == "Stella")
            {
                _highlighted = false;
                ActivateOutline(false);
                
                List<IItemEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemExit(this); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemExit(this); }, true);
            }
        }
        
        // For direct registration, ocassionally used
        List<IItemEventHandler> _registeredHandlers = new List<IItemEventHandler>(); 

        public void RegisterListener(IItemEventHandler handler)
        {
            _registeredHandlers.Add(handler);
        }

        public void UnRegisterListener(IItemEventHandler handler)
        {
            _registeredHandlers.Add(handler);
        }
    }
}