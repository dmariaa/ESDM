using UnityEngine.EventSystems;

namespace Maua
{
    public interface IMauaEventHandler : IEventSystemHandler
    {
        void PetalToggle(MauaPetal petal, bool open);
    }
}