using System.Collections.Generic;
using UnityEngine;
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
}