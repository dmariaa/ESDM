using ESDM.MenuSystem;
using ESDM.ScriptableObjects;
using ESDM.TutorialSystem;
using InventorySystem;
using ObjectHandlers;
using PlayerSystems;
using TutorialSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameController : MonoBehaviour, IMenuEventHandler, IInventoryPanelEventHandler, ITutorialEventHandler,
    IPlayerEventHandler, IDoorEventHandler
{
    public GameObject PlayerSpawnPoint;
    public GameObject PlayerCharacter;
    public GameObject InventoryPanel;
    public GameObject PausePanel;
    public GameObject TutorialRoomPanel;
    public GameObject QuestionsPanel;
    
    private bool gameIsPaused = false;
    
    private void Start()
    {
        Time.timeScale = 1;
        gameIsPaused = false;

        var currentGameData = GlobalGameState.Instance.CurrentGameState;
        
        if (!currentGameData.gameStarted)
        {
            currentGameData.gameStarted = true;
            currentGameData.tutorialPlayed = false;
            
            if (PlayerSpawnPoint)
            {
                PlayerCharacter.transform.localPosition = PlayerSpawnPoint.transform.localPosition;
            }
        }
        else
        {
            GetComponent<TutorialController>().alreadyRun = currentGameData.tutorialPlayed;
        
            if (currentGameData.tutorialPlayed)
            {
                PlayerCharacter.transform.localPosition = currentGameData.lastPosition;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalGameState.Instance.Save();
            PauseGame(!gameIsPaused);
        } 
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!gameIsPaused && InventoryPanel != null)
            {
                InventoryPanel.GetComponent<InventoryPanel>().TogglePanel();
            }
        }
    }

    private void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            gameIsPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gameIsPaused = false;
        }

        if (PausePanel != null)
        {
            PausePanel.SetActive(gameIsPaused);
        }
    }


    public void MenuSelected(string option)
    {
        switch (option)
        {
            case "back":
                EndGame();
                break;
            case "continue":
                PauseGame(false);
                break;
            default:
                Debug.LogFormat("Menu option {0} not handled by GlobalGameController", option);
                break;
        }
    }

    private void EndGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;

        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void InventoryPanelOpened()
    {
        PlayerCharacter.GetComponent<PlayerMovement>().paused = true;
    }

    public void InventoryPanelClosed()
    {
        PlayerCharacter.GetComponent<PlayerMovement>().paused = false;
    }

    public void TutorialEnded()
    {
        GlobalGameState.Instance.CurrentGameState.tutorialPlayed = true;
    }

    public void TutorialStepStarted()
    {
        throw new System.NotImplementedException();
    }

    public void TutorialStepFinished()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerMoved(Vector3 position)
    {
        GlobalGameState.Instance.CurrentGameState.lastPosition = position;
    }

    public void DoorHit()
    {
    }

    public void DoorOpened()
    {
        PlayerCharacter.GetComponent<PlayerMovement>().paused = true;
        QuestionsPanel?.SetActive(true);
    }

    public void ResetLevel()
    {   
        GlobalGameState.Instance.Save();
        EndGame();
    }
}
