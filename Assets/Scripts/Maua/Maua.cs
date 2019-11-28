using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maua
{
    public class Maua : MonoBehaviour, IPetalPointerEventHandler
    {
        public bool interactive = true;
        private string currentName = "";
        private string selectedPetal = "";
        private string sprite = "";
        public GameObject eventHandler;

        private void Start()
        {
            if(eventHandler==null)
            {
                eventHandler = ExecuteEvents.GetEventHandler<IMauaEventHandler>(transform.gameObject);
            }
            if (eventHandler == null)
            {
                Debug.Log("No event handler for maua events found.");
            }
            
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                Image childImage = child.GetComponent<Image>();

                if (childImage != null && childImage.sprite != null)
                {
                    child.GetComponent<Image>().alphaHitTestMinimumThreshold = 1;
                }
            }
        }
        
        public void PetalEnter(string name)
        {
            if (!interactive) return;
            
            if(name != currentName && name != selectedPetal)
            {
                GameObject petal = transform.Find(name).gameObject;
                petal.GetComponent<MauaPetal>().Show();
                currentName = name;
            }
        }

        public void PetalExit(string name)
        {
            if (!interactive) return;
            
            if(name != selectedPetal)
            {
                GameObject petal = transform.Find(name).gameObject;
                petal.GetComponent<MauaPetal>().Hide();
                currentName = "";
            }
        }

        public void PetalSelect(string name)
        {
            GameObject petal;

            if (name != selectedPetal)
            {
                string current = selectedPetal;
                selectedPetal = name;

                if (current != "")
                {
                    PetalExit(current);
                    
                    petal = transform.Find(current).gameObject;
                    
                    ExecuteEvents.Execute<IMauaEventHandler>(eventHandler, null,
                        (handler, data) => { handler.PetalToggle(petal.GetComponent<MauaPetal>(), false); });
                }
            }
            
            petal = transform.Find(name).gameObject;
            ExecuteEvents.Execute<IMauaEventHandler>(eventHandler, null,
                (handler, data) => { handler.PetalToggle(petal.GetComponent<MauaPetal>(), true); });

        }
    }
}
