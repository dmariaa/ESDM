using PlayerSystems;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    public abstract class TutorialAction : ScriptableObject
    {
        // Cached after first use
        protected GameObject panel;
        protected TutorialController controller;

        public abstract bool PlayAction();
        
        public virtual void InitAction(TutorialController controller)
        {
            this.controller = controller;
        }

        public virtual void ExitAction()
        {
            // Implement in child if needed
        }

        protected GameObject GetPanel(string name)
        {
            if (panel == null)
            {
                panel = controller?.TutorialPanel?.transform.Find(name)?.gameObject;
            }
            return panel;
        }

        protected void ShowPanel(string name)
        {
            PanelSetActive(name, true);
        }
        
        protected void HidePanel(string name)
        {
            PanelSetActive(name, false);
        }

        protected void PanelSetActive(string name, bool visible)
        {
            GetPanel(name)?.SetActive(visible);
        }

        protected void PauseCharacter(bool pause = true)
        {
            controller.Character.GetComponent<PlayerMovement>().paused = pause;
        }
    }
}