using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpInputHandler : MonoBehaviour
{
    public void ToggleHelp(bool show)
    {
        gameObject.SetActive(show);
    }
}
