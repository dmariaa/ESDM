using System.Collections;
using System.Collections.Generic;
using ESDM.MenuSystem;
using ESDM.ScriptableObjects;
using ESDM.TutorialSystem;
using Inventory.InventoryBar;
using InventorySystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameController : MonoBehaviour, IMenuEventHandler
{
    public GameObject PlayerCharacter;
    public GameObject InventoryPanel;
    public GameObject InventoryMaua;
    public GameObject PausePanel;
    public GameObject TutorialRoomPanel;
    
    private bool gameIsPaused = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gameIsPaused);
        } 
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!gameIsPaused && InventoryPanel != null)
            {
                InventoryPanel.GetComponent<InventoryPanel>().TogglePanel();
            }
        }

        if (InventoryPanel.GetComponent<InventoryPanel>().IsClosed())
        {
            PlayerCharacter.GetComponent<PlayerMovement>().paused = false;
        } else
        {
            PlayerCharacter.GetComponent<PlayerMovement>().paused = true;
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
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
