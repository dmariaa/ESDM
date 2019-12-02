using System;
using System.Collections;
using System.Collections.Generic;
using ESDM.MenuSystem;
using ESDM.Utilities;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ESDM.Items
{
    public class ItemTriggerHandler : MonoBehaviour
    {
        private bool highlighted = false;
        public InventoryItemTypes itemType;
        private Material material;
        private List<IItemPickupHandler> _registeredHandlers = new List<IItemPickupHandler>();

        public void ActivateOutline(bool active, bool animated = false, int speed = 0)
        {
            material.SetFloat("_Outline", active ? 1.0f : 0.0f);
            material.SetFloat("_OutlineAnimated", animated ? 1.0f : 0.0f);
            material.SetFloat("_OutlineSpeed", speed);
        }

        public void RegisterHandler(IItemPickupHandler handler)
        {
            _registeredHandlers.Add(handler);
        }
        
        public void UnRegisterHandler(IItemPickupHandler handler)
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
                
                List<IItemPickupHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemPickupHandler>();
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
                
                List<IItemPickupHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemPickupHandler>();
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
                List<IItemPickupHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IItemPickupHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, 
                    (handler, eventData) => { handler.ItemPickup(this.gameObject, this.itemType); });
                
                ExecuteEventHelper.BroadcastEvent(_registeredHandlers, 
                    (handler, eventData) => { handler.ItemPickup(this.gameObject, this.itemType); }, true);
            }
        }
    }
}

