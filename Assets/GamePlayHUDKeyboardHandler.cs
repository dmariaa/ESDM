using UnityEngine;

public class GamePlayHUDKeyboardHandler : MonoBehaviour
{
    public GameObject exitScene;
    private GameObject questionsPanel;
    private GameObject endLevelPanel;

    // Start is called before the first frame update
    void Start()
    {
        questionsPanel = transform.Find("QuestionsPanel").gameObject;
        endLevelPanel = transform.Find("EndLevelPanel").gameObject;
    }

    private void OnEnable()
    {
        questionsPanel?.SetActive(false);
        endLevelPanel?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (questionsPanel.active)
            {
                // Cannot exit from questions panel
            }
            else if (endLevelPanel.active)
            {
                gameObject.SetActive(false);
                exitScene.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(!questionsPanel.active)
            {
                questionsPanel.SetActive(true);
            }
        }
        else if(Input.anyKey)
        {
            if (endLevelPanel.active)
            {
                questionsPanel.SetActive(false);
                endLevelPanel.SetActive(false);
            }
        }
    }
}
