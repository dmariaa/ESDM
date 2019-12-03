using InventorySystem;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/OpenInventory")]
    public class OpenInventory : TutorialAction
    {
        private bool _triggered = false;
        
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            ShowPanel("OpenInventory");
            PauseCharacter(true);
        }

        public override bool PlayAction()
        {
            if (!_triggered)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    _triggered = true;
                }
            }
                
            if(_triggered && controller.InventoryPanel.GetComponent<InventoryPanel>().IsOpened()) {
                HidePanel("OpenInventory");
                PauseCharacter(false);
                return true;
            }

            return false;
        }
    }
}