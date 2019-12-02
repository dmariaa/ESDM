using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/MoveAround")]
    public class MoveAround : TutorialAction
    {
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            ShowPanel("MoveAround");
        }

        public override bool PlayAction()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.D))
            {
                HidePanel("MoveAround");
                return true;
            }

            return false;
        }
        
        
    }
}