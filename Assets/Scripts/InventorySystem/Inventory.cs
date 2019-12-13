using System.Collections.Generic;

namespace InventorySystem
{
    [System.Serializable]
    public class Inventory
    {
        public AbstractItem[] inventory = new AbstractItem[16];

        public int getLength()
        {
            return inventory.Length;
        }

        public bool SlotEmpty(int index)
        {
            return inventory[index] == null || inventory[index].ItemSprite == null;
        }

        public AbstractItem GetItem(int index)
        {
            if (SlotEmpty(index))
            {
                return null;
            }
            
            return inventory[index];
        }

        public int GetIndex(AbstractItem item)
        {
            for (int i = 0, length = inventory.Length; i < length; i++)
            {
                if (inventory[i] == item) return i;
            }

            return -1;
        }

        public int InsertItem(AbstractItem abstractItem)
        {
            int slot = FindEmptySlot();

            if (slot != -1)
            {
                inventory[slot] = abstractItem;

                foreach (IInventorySystemMessageHandler listener in _listeners)
                {
                    listener.ItemAdded(slot);
                }
            }

            return slot;
        }

        public bool RemoveItem(int index)
        {
            if (SlotEmpty(index))
            {
                return false;
            }

            inventory[index] = null;
            
            foreach (IInventorySystemMessageHandler listener in _listeners)
            {
                listener.ItemRemoved(index);
            }

            return true;
        }

        private int FindEmptySlot()
        {
            for (int i = 0, length = inventory.Length; i < length; i++)
            {
                if (SlotEmpty(i)) return i;
            }

            return -1;
        }
        
        // Messaging
        private List<IInventorySystemMessageHandler> _listeners = new List<IInventorySystemMessageHandler>();
        

        public void RegisterListener(IInventorySystemMessageHandler listener)
        {
            _listeners.Add(listener);
        }

        public void UnRegisterListener(IInventorySystemMessageHandler listener)
        {
            _listeners.Remove(listener);
        }
    }

    public interface IInventorySystemMessageHandler
    {
        void ItemAdded(int index);
        void ItemRemoved(int index);
    }
}
