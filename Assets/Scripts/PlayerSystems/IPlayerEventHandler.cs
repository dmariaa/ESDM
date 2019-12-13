using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerSystems
{
    public interface IPlayerEventHandler : IEventSystemHandler
    {
        void PlayerMoved(Vector3 position);
    }
}