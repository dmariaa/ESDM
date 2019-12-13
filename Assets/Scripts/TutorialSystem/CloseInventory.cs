using InventorySystem;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/CloseInventory")]
    public class CloseInventory : TutorialAction
    {
        private bool _triggered = false;
        
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            ShowPanel("CloseInventory");
            PauseCharacter(true);
            _triggered = false;
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
                HidePanel("CloseInventory");
                PauseCharacter(false);
                return true;
            }

            return false;
        }
    }
}