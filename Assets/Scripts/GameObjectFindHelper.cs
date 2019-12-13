using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace ESDM.Utilities
{
    public class GameObjectFindHelper
    {
        public static List<T> FindGameObjectWithInterface<T>()
        {
            List<T> interfaces = new List<T>();
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (GameObject rootGameObject in rootGameObjects)
            {
                T[] childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
                foreach (T childInterface in childrenInterfaces)
                {
                    interfaces.Add(childInterface);
                }
            }

            return interfaces;
        }
    }

    public class ExecuteEventHelper
    {
        public static void BroadcastEvent<T>(List<T> listeners, 
            ExecuteEvents.EventFunction<T> function, bool directCall = false) 
            where T : IEventSystemHandler
        {
            for (int i = 0, length = listeners.Count; i < length; i++)
            {
                if (!directCall)
                {
                    MonoBehaviour implementor = listeners[i] as MonoBehaviour;
                    ExecuteEvents.Execute<T>(implementor.gameObject, null, function);    
                }
                else
                {
                    T handler = listeners[i];
                    function(handler, null);
                }
                
            }
        }
    }
}