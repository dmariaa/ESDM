using System.IO;
using UnityEngine;

namespace ESDM.InventorySystem
{
    public class InventorySaveManager
    {
        private static string path = Path.Combine(Application.persistentDataPath, "inventory.json");

        public static void LoadOrInitializeInventory()
        {
            if (File.Exists(path))
            {
                Inventory.LoadFromJSON(path);
            }
            else
            {
                Inventory.InitializeFromDefault();
            }
        }

        public static void SaveInventory()
        {
            Inventory.Instance.SaveToJSON(path);    
        }
    }
}