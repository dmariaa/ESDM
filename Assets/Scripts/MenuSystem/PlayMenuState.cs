using ESDM.MenuSystem;
using ESDM.ScriptableObjects;
using UnityEngine;

public class PlayMenuState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalGameState.Instance.CurrentGameState.gameStarted)
        {
            Menu menu = GetComponentInChildren<Menu>();
            menu.RemoveMenuOption("continue");
        }
    }
}
