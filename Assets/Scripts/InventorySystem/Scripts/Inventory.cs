using UnityEngine;

namespace ESDM.InventorySystem
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
                    Inventory[] tmp = Resources.FindObjectsOfTypeAll<Inventory>();
                    if (tmp.Length > 0)
                    {
                        _instance = tmp[0];
                    }
                    else
                    {
                        InventorySaveManager.LoadOrInitializeInventory();    
                    }
                }

                return _instance;
            }
        }
        
        public static void InitializeFromDefault()
        {
            if(_instance) DestroyImmediate(_instance);
            _instance = Instantiate((Inventory) Resources.Load("InventoryTemplate"));
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
            return inventory[index] == null || inventory[index].ItemGameObject == null;
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
    }
}
