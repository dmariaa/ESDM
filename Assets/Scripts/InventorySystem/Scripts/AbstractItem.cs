using UnityEngine;

namespace ESDM.InventorySystem
{
    [System.Serializable]
    public abstract class AbstractItem : ScriptableObject
    {
        public string Name;
        [TextArea(15, 20)] public string Description;
        public Sprite ItemGameObject;
    }
}