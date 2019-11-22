using System;
using System.Collections;
using System.Collections.Generic;
using Iventory.InventoryBar;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    private void Start()
    { 
        Toggle toggle = GetComponent<Toggle>(); 
        toggle.onValueChanged.AddListener( delegate { ToggleValueChanbed(toggle); });
    }

    void ToggleValueChanbed(Toggle change)
    {
        foreach (Inventory.Inventory target in FindObjectsOfType<Inventory.Inventory>())
        {
            ExecuteEvents.Execute<IInventoryEventHandler>(target.gameObject, null,
                (handler, eventData) => { handler.ItemSelected(transform.gameObject, change.isOn); });
        }
    }
}
