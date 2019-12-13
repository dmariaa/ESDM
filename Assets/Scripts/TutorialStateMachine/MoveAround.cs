using UnityEngine;

namespace ESDM.TutorialStateMachine
{
    public class MoveAround : StateMachineBehaviour
    {
        private GameObject key;
        private GameObject character;
        private GlobalGameController gameController;
        private GameObject tutorialRoomCanvas;
        private bool paused;

        private Vector2 keyPosition, characterPosition;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameController = animator.gameObject.GetComponent<GlobalGameController>();
            tutorialRoomCanvas = gameController.TutorialRoomPanel;
            tutorialRoomCanvas.transform.Find("MoveAround").gameObject.SetActive(true);

            key = GameObject.Find("I_Key01");
            character = GameObject.Find("PlayerCharacter");
            
            keyPosition = new Vector2(key.transform.localPosition.x, key.transform.localPosition.y);

            Time.timeScale = 0;
            paused = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            tutorialRoomCanvas.transform.Find("MoveAround").gameObject.SetActive(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (paused && Input.anyKey)
            {
                Time.timeScale = 1;
                paused = false;
                animator.SetBool("SeeObject", true);
            }
        }
    }
}

