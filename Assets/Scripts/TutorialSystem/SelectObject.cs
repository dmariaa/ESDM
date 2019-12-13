using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/SelectObject")]
    public class SelectObject : TutorialAction
    {
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            PauseCharacter(true);
            ShowPanel("SelectObject");
        }

        public override bool PlayAction()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HidePanel("SelectObject");
                PauseCharacter(false);
                return true;
            }

            return false;
        }
    }
}