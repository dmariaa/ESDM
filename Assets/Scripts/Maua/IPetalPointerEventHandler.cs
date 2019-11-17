using UnityEngine.EventSystems;

namespace Maua
{
    public interface IPetalPointerEventHandler : IEventSystemHandler
    {
        void PetalEnter(string name);
        void PetalExit(string name);
    }
}