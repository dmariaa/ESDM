using ESDM.ScriptableObjects;
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
                case "continue":
                    RunGame();
                    break;
                case "new-game":
                    GlobalGameState.Instance.ResetCurrentGame();
                    break;
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

        private Menu GetMenu(string name)
        {
            GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
            for (int i = 0, length = rootGameObjects.Length; i < length; i++)
            {
                if (rootGameObjects[i].name == name)
                {
                    return rootGameObjects[i].GetComponentInChildren<Menu>();
                }
            }

            return null;
        }
    }
}