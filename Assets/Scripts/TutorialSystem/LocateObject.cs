using InventorySystem;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/LocateObject")]
    public class LocateObject : TutorialAction
    {
        public float triggerDistance = 4.0f;
        
        private bool triggered = false;

        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            triggered = false;
        }

        public override bool PlayAction()
        {
            float distance = (controller.Character.transform.localPosition - 
                              controller.Object.transform.localPosition).sqrMagnitude;
            
            if (distance <= triggerDistance)
            {
                if (!triggered)
                {
                    ShowPanel("SeeObject");
                    controller.Character.GetComponent<PlayerMovement>().paused = true;
                    controller.Object.GetComponent<ItemGameObject>().ActivateOutline(true, true, 3);
                    triggered = false;
                }

                if (Input.anyKeyDown)
                {
                    HidePanel("SeeObject");
                    controller.Character.GetComponent<PlayerMovement>().paused = false;
                    controller.Object.GetComponent<ItemGameObject>().ActivateOutline(false);
                    triggered = false;
                    return true;
                }
            }

            return false;
        }
    }
}