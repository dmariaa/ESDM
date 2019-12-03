using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ESDM.Items
{
    public interface IItemEventHandler : IEventSystemHandler
    {
        void ItemPickup(GameObject item, InventoryItemTypes inventoryItemType);
        void ItemEnter(GameObject item, InventoryItemTypes inventoryItemType);
        void ItemExit(GameObject item, InventoryItemTypes inventoryItemType);
    }
}