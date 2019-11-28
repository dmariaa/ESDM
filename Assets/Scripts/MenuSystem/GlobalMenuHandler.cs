using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ESDM.MenuSystem
{
    public class GlobalMenuHandler : MonoBehaviour, IMenuEventHandler
    {
        public void MenuSelected(string option)
        {
            switch (option)
            {
                case "exit":
                    QuitApplication();
                    break;
                default:
                    Debug.LogFormat("No menu event global handler in GlobalMenuHandler for menu {0}", option);
                    break;
            }
        }

        public void RunGame()
        {
            SceneManager.LoadScene("Scenes/GameScene");
        }

        private void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}