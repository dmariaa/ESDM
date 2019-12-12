using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/GetHelp")]
    public class GetHelp : TutorialAction, IKeyboardHelpPanelHandler
    {
        private bool _triggered = false;
        private bool _opened = false;
        
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            ShowPanel("GetHelp");
            PauseCharacter(true);
            _triggered = false;
            
            controller.HelpPanel.GetComponent<KeyboardHelpPanel>().RegisterListener(this);
        }

        public override bool PlayAction()
        {
            if(_triggered && !_opened)
            {
                HidePanel("GetHelp");
                PauseCharacter(false);
                _triggered = false;
                
                controller.HelpPanel.GetComponent<KeyboardHelpPanel>().UnRegisterListener(this);
                return true;
            }

            return false;
        }

        public void KeyboardHelpPanelToggle(bool open)
        {
            if (open)
            {
                Debug.Log("Help panel opened");
                _triggered = true;
                _opened = true;
            }
            else
            {
                Debug.Log("Help panel closed");
                _opened = false;
            }
        }
    }
}