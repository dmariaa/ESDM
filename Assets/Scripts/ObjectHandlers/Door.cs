using System.Collections.Generic;
using ConversationSystem.Behaviours;
using ESDM.Utilities;
using Maua;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ObjectHandlers
{
    public class Door : MonoBehaviour, IMauaEventHandler
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Stella")
            {
                TextManager textManager = other.GetComponent<TextManager>();
                textManager.DisplayMessage("Necesito algo para abrir esta puerta", Color.white, 0);
                textManager.DisplayMessage("", Color.white, 3.0f);
                
                List<IDoorEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IDoorEventHandler>();
                ExecuteEventHelper.BroadcastEvent(handlers, (handler, eventData) => { handler.DoorHit(); });
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "Stella")
            {
            }
        }

        public void PetalToggle(MauaPetal petal, bool open)
        {
            List<IDoorEventHandler> handlers = GameObjectFindHelper.FindGameObjectWithInterface<IDoorEventHandler>();
            ExecuteEventHelper.BroadcastEvent(handlers, (handler, eventData) => { handler.DoorOpened(); });
        }
    }

    public interface IDoorEventHandler : IEventSystemHandler
    {
        void DoorHit();
        void DoorOpened();
    }
}