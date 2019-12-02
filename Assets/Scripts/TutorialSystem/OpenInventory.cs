using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/OpenInventory")]
    public class OpenInventory : TutorialAction
    {
        private float triggerTime = 1.0f;
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            ShowPanel("OpenInventory");
            controller.Character.GetComponent<PlayerMovement>().paused = true;
        }

        public override bool PlayAction()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                controller.Character.GetComponent<PlayerMovement>().paused = false;
                HidePanel("OpenInventory");
                return true;
            }

            return false;
        }
    }
}