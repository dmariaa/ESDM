using UnityEngine.EventSystems;

namespace TutorialSystem
{
    public interface ITutorialEventHandler : IEventSystemHandler
    {
        void TutorialEnded();
        void TutorialStepStarted();
        void TutorialStepFinished();
    }
}