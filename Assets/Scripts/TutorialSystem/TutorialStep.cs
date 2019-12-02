using System;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Tutorial Step")]
    public class TutorialStep : ScriptableObject
    {
        public TutorialStep nextStep;
        public TutorialAction action;
        
        [NonSerialized]
        private bool Initialized = false;

        public void UpdateStep(TutorialController controller)
        {
            if (!Initialized)
            {
                Debug.LogFormat("Initializing state {0}", action.name);
                action.InitAction(controller);
                Initialized = true;
            }
            
            if (action.PlayAction())
            {
                if (nextStep != null)
                {
                    Debug.LogFormat("Switching to state {0}", nextStep.name);    
                }
                
                action.ExitAction();
                controller.ChangeStep(nextStep);
                Initialized = false;
            }
        }
    }
}