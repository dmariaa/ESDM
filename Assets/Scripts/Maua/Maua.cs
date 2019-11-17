using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maua
{
    public class Maua : MonoBehaviour, IPetalPointerEventHandler
    {
        public bool interactive = true;
        private string currentName = "";
        
        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                Image childImage = child.GetComponent<Image>();
                if (childImage.sprite != null)
                {
                    child.GetComponent<Image>().alphaHitTestMinimumThreshold = 1;    
                }
            }
        }
        
        public void PetalEnter(string name)
        {
            if (!interactive) return;
            
            if(name != currentName)
            {
                GameObject petal = transform.Find(name).gameObject;
                petal.GetComponent<MauaPetal>().Show();
                currentName = name;
            }
        }

        public void PetalExit(string name)
        {
            if (!interactive) return;
            
            GameObject petal = transform.Find(name).gameObject;
            petal.GetComponent<MauaPetal>().Hide();
            currentName = "";
        }
    }

}
