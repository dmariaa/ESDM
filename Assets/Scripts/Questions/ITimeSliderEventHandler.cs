using UnityEngine.EventSystems;

namespace Questions
{
    public interface ITimeSliderEventHandler : IEventSystemHandler
    {
        void TimeEnd();
    }
}