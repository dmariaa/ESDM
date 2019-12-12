using PlayerSystems;
using UnityEngine;

namespace ESDM.TutorialStateMachine
{
    public class SelectObject : StateMachineBehaviour
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

            key = GameObject.Find("I_Key01");
            character = GameObject.Find("PlayerCharacter");
            keyPosition = new Vector2(key.transform.localPosition.x, key.transform.localPosition.y);

            paused = false;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            tutorialRoomCanvas.transform.Find("SelectObject").gameObject.SetActive(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            characterPosition = new Vector2(character.transform.localPosition.x, character.transform.localPosition.y);

            if (paused)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    character.GetComponent<PlayerMovement>().paused = false;
                    tutorialRoomCanvas.transform.Find("SelectObject").gameObject.SetActive(false);
                    animator.SetBool("OpenInventory", true);
                }                
            } 
            else if ((keyPosition - characterPosition).sqrMagnitude <= 0.1f)
            {
                character.GetComponent<PlayerMovement>().paused = true;
                tutorialRoomCanvas.transform.Find("SelectObject").gameObject.SetActive(true);
                paused = true;
            }
        }
    }
}

