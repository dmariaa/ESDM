using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "ESDM/Inventory", fileName = "Inventory.asset")]
    public class Inventory : ScriptableObject
    {
        private static Inventory _instance;

        public static Inventory Instance
        {
            get
            {
                if (!_instance)
                {
                    InventorySaveManager.LoadOrInitializeInventory();    
                }

                return _instance;
            }
        }
        
        public static void InitializeFromDefault()
        {
            if(_instance) DestroyImmediate(_instance);
            Inventory inventory = Resources.Load<Inventory>("Inventory/InventoryTemplate");
            _instance = Instantiate(inventory);
            _instance.hideFlags = HideFlags.HideAndDontSave;
        }

        public static void LoadFromJSON(string path)
        {
            if(_instance) DestroyImmediate(_instance);
            _instance = ScriptableObject.CreateInstance<Inventory>();
            JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), _instance);
            _instance.hideFlags = HideFlags.HideAndDontSave;
        }

        public void SaveToJSON(string path)
        {
            System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
        }

        public AbstractItem[] inventory;

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

        public int InsertItem(AbstractItem abstractItem)
        {
            int slot = FindEmptySlot();

            if (slot != -1)
            {
                inventory[slot] = abstractItem;

                foreach (IInventorySystemMessageHandler listener in listeners)
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
            
            foreach (IInventorySystemMessageHandler listener in listeners)
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
        private List<IInventorySystemMessageHandler> listeners = new List<IInventorySystemMessageHandler>();

        public void RegisterListener(IInventorySystemMessageHandler listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(IInventorySystemMessageHandler listener)
        {
            listeners.Remove(listener);
        }
    }

    public interface IInventorySystemMessageHandler
    {
        void ItemAdded(int index);
        void ItemRemoved(int index);
    }
}
