using TutorialSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ESDM.TutorialSystem
{
    public class TutorialController : MonoBehaviour
    {
        public GameObject HelpPanel;
        public GameObject Character;
        public GameObject Object;
        public GameObject InventoryPanel;
        public GameObject TutorialPanel;
        public TutorialStep step;
        
        private bool running = false;
        
        public bool alreadyRun = false;

        private void Update()
        {
            if (running && !alreadyRun)
            {
                step.UpdateStep(this);    
            }
        }

        public void Start()
        {
            if (step != null)
            {
                running = true;    
            }
        }

        public void ChangeStep(TutorialStep nextStep)
        {
            if (nextStep != null)
            {
                step = nextStep;
            }
            else
            {
                running = false;
                GlobalGameController gameController = transform.GetComponent<GlobalGameController>();
                ExecuteEvents.Execute<ITutorialEventHandler>(gameObject, null,
                    (handler, eventData) => { handler.TutorialEnded(); });
            }
        }
    }
}