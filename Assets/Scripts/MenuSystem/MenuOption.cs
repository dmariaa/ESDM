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
        public bool isSpacer = false;
        public Canvas destination;

        [HideInInspector]
        public GameObject GameObject;
    };
}