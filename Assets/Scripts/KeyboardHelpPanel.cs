using System.Collections.Generic;
using ESDM.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardHelpPanel : MonoBehaviour
{
    private GameObject _helpPanel;
    private bool _opened = false;

    private void Awake()
    {
        _helpPanel = transform.GetChild(0).gameObject;
    }

    public void ToggleHelp(bool show)
    {
        _helpPanel.SetActive(show);
        
        // Call gameobject listeners
        List<IKeyboardHelpPanelHandler> listeners = GameObjectFindHelper.FindGameObjectWithInterface<IKeyboardHelpPanelHandler>();
        ExecuteEventHelper.BroadcastEvent(listeners, (handler, data) => { handler.KeyboardHelpPanelToggle(show); });
        
        // Call scriptable object listeners
        ExecuteEventHelper.BroadcastEvent(_listeners, (handler, data) => { handler.KeyboardHelpPanelToggle(show);}, true);
    }

    private void Update()
    {
        if (!_opened && Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Opened help panel");
            ToggleHelp(true);
            _opened = true;
        } else if (_opened && Input.GetKeyUp(KeyCode.H))
        {
            Debug.Log("Closed help panel");
            ToggleHelp(false);
            _opened = false;
        }
    }

    private List<IKeyboardHelpPanelHandler> _listeners = new List<IKeyboardHelpPanelHandler>();

    public void RegisterListener(IKeyboardHelpPanelHandler listener)
    {
        _listeners.Add(listener);
    }
    
    public void UnRegisterListener(IKeyboardHelpPanelHandler listener)
    {
        _listeners.Remove(listener);
    }
}

public interface IKeyboardHelpPanelHandler : IEventSystemHandler
{
    void KeyboardHelpPanelToggle(bool open);
}

