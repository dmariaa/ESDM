using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameButtonHandler : MonoBehaviour
{
    public Canvas destination;
    // Start is called before the first frame update
    void Start()
    {
        Transform playButton = transform.Find("PlayButton");
        Button button = playButton.GetComponent<Button>();
        button.onClick.AddListener(PlayButtonClick);
    }

    void PlayButtonClick()
    {
        if(destination != null)
        {
            gameObject.SetActive(false);
            destination.gameObject.SetActive(true);
        }
    }
}
