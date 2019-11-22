using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayHUDKeyboardHandler : MonoBehaviour
{
    public GameObject exitScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            exitScene.SetActive(true);
        }
    }
}
