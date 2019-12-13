using UnityEngine;


namespace ESDM.TutorialStateMachine
{
    public class PickupObject : StateMachineBehaviour
    {
        private GameObject character;
        private GlobalGameController gameController;
        private GameObject tutorialRoomCanvas;
        private bool paused;
        
            
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameController = animator.gameObject.GetComponent<GlobalGameController>();
            tutorialRoomCanvas = gameController.TutorialRoomPanel;
            character = GameObject.Find("PlayerCharacter");
            tutorialRoomCanvas.transform.Find("PickupObject").gameObject.SetActive(true);
            
            paused = false;
        }
    
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
    
        }
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
    
        }
    }
}
