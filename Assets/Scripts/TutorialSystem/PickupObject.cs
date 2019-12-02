using System;
using ESDM.Items;
using Inventory;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/PickupObject")]
    public class PickupObject : TutorialAction, IItemPickupHandler
    {
        [NonSerialized] private bool itemPickedUp;
        
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            
            itemPickedUp = false;
            controller.Object.GetComponent<ItemTriggerHandler>().RegisterHandler(this);
        }

        public override void ExitAction()
        {
            base.ExitAction();
            
            controller.Object.GetComponent<ItemTriggerHandler>().UnRegisterHandler(this);
        }

        public override bool PlayAction()
        {
            return itemPickedUp;
        }

        public void ItemPickup(GameObject item, InventoryItemTypes inventoryItemType)
        {
            HidePanel("SelectObject");
            controller.Character.GetComponent<PlayerMovement>().paused = false;
            itemPickedUp = true;
        }

        public void ItemEnter(GameObject item, InventoryItemTypes inventoryItemType)
        {
            ShowPanel("PickupObject");
            controller.Character.GetComponent<PlayerMovement>().paused = true;
        }

        public void ItemExit(GameObject item, InventoryItemTypes inventoryItemType)
        {
        }
    }
}