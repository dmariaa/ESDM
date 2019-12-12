using PlayerSystems;
using UnityEngine;

namespace ESDM.TutorialStateMachine
{
    public class SeeObject : StateMachineBehaviour
    {
        private GameObject key;
        private GameObject character;
        private GlobalGameController gameController;
        private GameObject tutorialRoomCanvas;
        private bool paused;
        private float time = 0.0f;

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
            tutorialRoomCanvas.transform.Find("SeeObject").gameObject.SetActive(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {        
            characterPosition = new Vector2(character.transform.localPosition.x, character.transform.localPosition.y);

            if (paused)
            {
                time += Time.deltaTime;
                Debug.LogFormat("Timer: {0}", time);

                if (time >= 2.0f)
                {
                    character.GetComponent<PlayerMovement>().paused = false;
                    animator.SetBool("SelectObject", true);
                }
            }
            else if ((keyPosition - characterPosition).sqrMagnitude <= 5.0f)
            {
                character.GetComponent<PlayerMovement>().paused = true;
                tutorialRoomCanvas.transform.Find("SeeObject").gameObject.SetActive(true);
                paused = true;
            }
        }
    }    
}

