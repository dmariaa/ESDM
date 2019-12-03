using System.Collections.Generic;
using ESDM.Utilities;
using Inventory;
using UnityEngine;

namespace ESDM.Items
{
    public class ItemTriggerHandler : MonoBehaviour
    {
        private bool highlighted = false;
        public InventoryItemTypes itemType;
        private Material material;
        private List<IItemEventHandler> _registeredHandlers = new List<IItemEventHandler>();

        public void ActivateOutline(bool active, bool animated = false, int speed = 0)
        {
            material.SetFloat("_Outline", active ? 1.0f : 0.0f);
            material.SetFloat("_OutlineAnimated", animated ? 1.0f : 0.0f);
            material.SetFloat("_OutlineSpeed", speed);
        }

        public void RegisterHandler(IItemEventHandler handler)
        {
            _registeredHandlers.Add(handler);
        }
        
        public void UnRegisterHandler(IItemEventHandler handler)
        {
            _registeredHandlers.Remove(handler);
        }

        private void Start()
        {
            material = GetComponent<SpriteRenderer>().material;
            highlighted = false;
            ActivateOutline(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.name=="Stella")
            {
                Debug.Log("Activating item halo");
                highlighted = true;
                ActivateOutline(true);
                
                List<IItemEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemEnter(this.gameObject, this.itemType); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemEnter(this.gameObject, this.itemType); }, true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.name=="Stella")
            {
                Debug.Log("Deactivating item halo");
                ActivateOutline(false);
                highlighted = false;
                
                List<IItemEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemExit(this.gameObject, this.itemType); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemExit(this.gameObject, this.itemType); }, true);
            }
        }

        private void Update()
        {
            if (highlighted && Input.GetKeyDown(KeyCode.Space))
            {
                List<IItemEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemPickup(this.gameObject, this.itemType); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemPickup(this.gameObject, this.itemType); }, true);
            }
        }
    }
}

