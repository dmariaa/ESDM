using UnityEngine.EventSystems;

namespace ESDM.MenuSystem
{
    public interface IMenuEventHandler : IEventSystemHandler
    {
        void MenuSelected(string option);
    }
}