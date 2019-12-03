using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    public interface IItemEventHandler : IEventSystemHandler
    {
        void ItemPickup(ItemGameObject item);
        void ItemEnter(ItemGameObject item);
        void ItemExit(ItemGameObject item);
    }
}