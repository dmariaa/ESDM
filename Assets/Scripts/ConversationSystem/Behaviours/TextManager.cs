using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConversationSystem.Behaviours
{
    public class TextManager : UnityEngine.MonoBehaviour
    {
        public struct Instruction
        {
            public string message;
            public Color textColor;
            public float startTime;
        }

        public Text text;
        public float displayTimePerCharacter = 0.1f;
        public float additionalDisplayTime = 0.5f;

        private List<Instruction> instructions = new List<Instruction>();
        private float clearTime;

        private void Update()
        {
            if (instructions.Count > 0 && Time.time >= instructions[0].startTime)
            {
                text.text = instructions[0].message;
                text.color = instructions[0].textColor;
                instructions.RemoveAt(0);
                Debug.Log("Sent message " + text.text);
                
                ShowTextPanel();
            }

            if (instructions.Count == 0)
            {
                text.text = "";
                ShowTextPanel(false);
            }
        }

        private void ShowTextPanel(bool show = true)
        {
            var textPanel = text.transform.parent.gameObject;

            if (show)
            {
                textPanel.SetActive(true);    
            }
            else
            {
                textPanel.SetActive(false);
            }
            
        }

        public void DisplayMessage(string message, Color textColor, float delay)
        {
            float startTime = Time.time + delay;
            float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;
            float newClearTime = startTime + displayDuration;

            if (newClearTime > clearTime)
                clearTime = newClearTime;

            Instruction newInstruction = new Instruction
            {
                message = message,
                textColor = textColor,
                startTime = startTime
            };

            instructions.Add (newInstruction);

            SortInstructions ();
        }
        
        private void SortInstructions ()
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                bool swapped = false;

                for (int j = 0; j < instructions.Count; j++)
                {
                    if (instructions[i].startTime > instructions[j].startTime)
                    {
                        Instruction temp = instructions[i];
                        instructions[i] = instructions[j];
                        instructions[j] = temp;

                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }
    }
}