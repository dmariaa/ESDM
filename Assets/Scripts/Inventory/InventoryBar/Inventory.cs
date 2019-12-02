using System;
using System.Collections.Generic;
using ESDM.ScriptableObjects;
using Inventory.InventoryBar;
using Iventory.InventoryBar;
using Maua;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour, IInventoryEventHandler, IPanelSliderEventHandler, IMauaEventHandler
    {
        public List<InventoryItemTypes> InventoryItems = new List<InventoryItemTypes>();
        public GameObject InformationPanel;
        private GameObject itemSelected;
        private GameObject buttonPrefab;

        private void Start()
        {
            buttonPrefab = Resources.Load<GameObject>("Prefabs/InventoryButton");
            
            foreach (InventoryItemTypes type in InventoryItems)
            {
                GameObject button = Instantiate(buttonPrefab, transform);
                InventoryItem buttonItem = button.AddComponent<InventoryItem>();
                buttonItem.ItemType = type;
                button.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
            }
            
            InformationPanel.SetActive(false);
        }

        public void ItemAdd(InventoryItemTypes itemType)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            InventoryItem buttonItem = button.AddComponent<InventoryItem>();
            buttonItem.ItemType = itemType;
            button.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
        }

        public void ItemSelected(GameObject item, bool selected)
        {
            if (selected)
            {
                itemSelected = item;
                InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
                GameObject icon = InformationPanel.transform.Find("Icon").gameObject;
                GameObject name = InformationPanel.transform.Find("Name").gameObject;
                GameObject description = InformationPanel.transform.Find("Description").gameObject;
                icon.GetComponent<Image>().sprite = inventoryItem.item.GetComponent<Image>().sprite;
                name.GetComponent<Text>().text = inventoryItem.Name;
                description.GetComponent<Text>().text = inventoryItem.Description;
                InformationPanel.SetActive(true);
            }
            else
            {
                itemSelected = null;
            }
            
            InformationPanel.SetActive(selected);
        }

        public void PanelSlide(bool open)
        {
            if (!open)
            {
                InformationPanel.SetActive(false);
            }
        }

        public void PetalToggle(MauaPetal petal, bool open)
        {
            if (open && itemSelected != null)
            {
                InventoryItem inventoryItem = itemSelected.GetComponent<InventoryItem>();

                MauaPetal currentPetal = itemsInPetals.ContainsKey(inventoryItem.Name) ? itemsInPetals[inventoryItem.Name] : null;
                if (currentPetal != null)
                {
                    currentPetal.SetPetalImage(null);                        
                }
                
                GameObject item = inventoryItem.item;
                petal.SetPetalImage(item.GetComponent<Image>().sprite);
                itemsInPetals[inventoryItem.Name] = petal;
            }
        }
        
        private Dictionary<string, MauaPetal> itemsInPetals = new Dictionary<string, MauaPetal>();
    }
}