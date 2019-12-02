using System;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    public class TutorialController : MonoBehaviour
    {
        public GameObject Character;
        public GameObject Object;
        public GameObject TutorialPanel;
        public TutorialStep step;
        private bool running = false;

        private void Update()
        {
            if (running)
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
            }
        }
    }
}