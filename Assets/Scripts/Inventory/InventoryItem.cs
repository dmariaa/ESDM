using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public InventoryItemTypes ItemType = InventoryItemTypes.GREEN_POTION;
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public GameObject item { get; private set; }

        private void Start()
        {
            InventoryItemType inventoryItemType = InventoryItemType.Create(ItemType);
            item = FindItem();
            item.GetComponent<Image>().sprite = inventoryItemType.Sprite;
            Name = inventoryItemType.Name;
            Description = inventoryItemType.Description;
            
            Color color = Color.white;
            color.a = inventoryItemType.Sprite==null ? 0.0f : 1.0f;
            item.GetComponent<Image>().color = color;
        }

        private GameObject FindItem()
        {
            Transform[] children = transform.GetComponentsInChildren<Transform> ();
            foreach (var child in children) {
                if (child.name == "Item")
                {
                    return child.gameObject;
                }
            }

            return null;
        }
    }

    public enum InventoryItemTypes
    {
        GREEN_POTION,
        ORANGE_POTION,
        YELLOW_POTION,
        POISON_03,
        POISON_05,
        BOOK_05,
        BOOK_07,
        KEY_01
    }


    public struct InventoryItemType
    {
        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private InventoryItemType(string name, string description, string sprite)
        {
            Sprite = Resources.Load<Sprite>("Images/Items/" + sprite);
            Name = name;
            Description = description;
        }

        public static InventoryItemType Create(InventoryItemTypes type)
        {
            switch (type)
            {
                case InventoryItemTypes.GREEN_POTION:
                    return new InventoryItemType("Pocima de salud", "Pocima que aumenta la salud", "P_Green03");
                case InventoryItemTypes.ORANGE_POTION:
                    return new InventoryItemType("Pocima de energia", "Pocima que aumenta la energía", "P_Orange03");
                case InventoryItemTypes.YELLOW_POTION:
                    return new InventoryItemType("Pocima amarilla", "Pocima que aumenta el valor", "P_Yellow08");
                case InventoryItemTypes.POISON_03:
                    return new InventoryItemType("Hiedra venenosa", "Veneno en forma de hoja", "S_Poison03");
                case InventoryItemTypes.POISON_05:
                    return new InventoryItemType("Musgo venenoso", "Veneno en forma de musgo", "S_Poison05");
                case InventoryItemTypes.BOOK_05:
                    return new InventoryItemType("El secreto de Mirazcar", "El libro misterioso", "W_Book05");
                case InventoryItemTypes.BOOK_07:
                    return new InventoryItemType("Mapa de carreteras", "Un mapa de carreteras", "W_Book07");
                case InventoryItemTypes.KEY_01:
                    return new InventoryItemType("Llave", "Una llave para una puert", "I_Key01");
                default: 
                    return new InventoryItemType("", "", "");
            }
        }
    }
}