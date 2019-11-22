using UnityEngine;
using UnityEngine.EventSystems;

namespace Iventory.InventoryBar
{
    public interface IInventoryEventHandler : IEventSystemHandler
    {
        void ItemSelected(GameObject item, bool selected);
    }
}