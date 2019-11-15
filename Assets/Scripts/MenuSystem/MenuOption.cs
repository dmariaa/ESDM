using System;
using UnityEngine;

namespace ESDM.MenuSystem
{
    [System.Serializable]
    public class MenuOption
    {
        public enum MenuOptionType
        {
            Canvas, 
            Script, 
            Event
        };
        
        public string name;
        public string label;
        public MenuOptionType Type;
        public Canvas destination;
    };
}