using System.Collections.Generic;
using ESDM.MenuSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Questions
{
    public class QuestionsManager : MonoBehaviour, ITimeSliderEventHandler
    {
        public List<Question> questions = new List<Question>();
        private int currentQuestion = 0;
        public GameObject endLevelPanel;

        private void Start()
        {
        }

        private void OnEnable()
        {
            Debug.Log("Starting test");
            StartTest();
        }

        public void StartTest()
        {
            currentQuestion = 0;
            GenerateQuestion(currentQuestion);
        }

        private void GenerateQuestion(int index)
        {
            Question question = questions[index];

            transform.Find("InsidePanel/Question").GetComponent<Text>().text = question.QuestionText;
            Menu panelMenu = transform.Find("InsidePanel/AnswersPanel").GetComponent<Menu>();
            panelMenu.options.Clear();
            int i = 0;
            
            foreach (string questionAnswer in question.QuestionAnswers)
            {
                MenuOption menuOption = new MenuOption();
                menuOption.name = "answer-" + (++i);
                menuOption.label = questionAnswer;
                panelMenu.options.Add(menuOption);        
            }
            
            panelMenu.Restart();
            
            transform.Find("InsidePanel/TimeBarPanel/TimeSlider").GetComponent<TimeSlider>().Restart();
        }
        
        public void TimeEnd()
        {
            currentQuestion++;
            if (currentQuestion < questions.Count)
            {
                GenerateQuestion(currentQuestion);
            }
            else
            {
                gameObject.SetActive(false);
                endLevelPanel.SetActive(true);
            }
        }
    }
}
