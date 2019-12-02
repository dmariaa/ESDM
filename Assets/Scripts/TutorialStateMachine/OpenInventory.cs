using UnityEngine;

namespace ESDM.TutorialStateMachine
{
    public class OpenInventory : StateMachineBehaviour
    {
        private GameObject key;
        private GameObject character;
        private GlobalGameController gameController;
        private GameObject tutorialRoomCanvas;
        private bool paused;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            gameController = animator.gameObject.GetComponent<GlobalGameController>();
            tutorialRoomCanvas = gameController.TutorialRoomPanel;
            character = GameObject.Find("PlayerCharacter");

            tutorialRoomCanvas.transform.Find("OpenInventory").gameObject.SetActive(true);
            character.GetComponent<PlayerMovement>().paused = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            tutorialRoomCanvas.transform.Find("OpenInventory").gameObject.SetActive(true);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (paused && Input.GetKeyDown(KeyCode.Tab))
            {
                paused = false;
                animator.SetBool("PickupObject", true);
                character.GetComponent<PlayerMovement>().paused = false;
                tutorialRoomCanvas.transform.Find("OpenInventory").gameObject.SetActive(false);
            }
        }
    }
}