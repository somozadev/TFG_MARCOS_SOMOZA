using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DeviceController : MonoBehaviour
{
    public DevicesDictionaryCompound currentDevices;
    public event Action gamepadEvent;
    public event Action keyboardEvent;

    private void Start()
    {
        InitCurrentDevices();
    }


    public void InitCurrentDevices()
    {
        if (Gamepad.current != null)
            currentDevices = new DevicesDictionaryCompound(Keyboard.current.name, Keyboard.current.enabled, Mouse.current.name, Mouse.current.enabled, Gamepad.current.name, Gamepad.current.enabled);
        else
            currentDevices = new DevicesDictionaryCompound(Keyboard.current.name, Keyboard.current.enabled, Mouse.current.name, Mouse.current.enabled, "disconnected Gamepad", false);


    }

    void Update()
    {
        if (Gamepad.current != null && !currentDevices.devices[2].enabled)
        {
            SwitchToGamepad();
            InitCurrentDevices();
            Debug.Log("<><><><><><><><><><><><<><>><><>");
        }

        else if (Gamepad.current == null && !currentDevices.devices[0].enabled)
        {
            SwitchToKeyboard();
            InitCurrentDevices();
            Debug.Log("[][][[][][][]][][][][][][][][]]");
        }
    }



    private void SwitchToGamepad()
    {
        int count = SceneManager.sceneCount;
        bool canEvent = false;
        Debug.Log("count: " + count);
        for (int i = 0; i < count; i++)
        {
            string name = SceneManager.GetSceneAt(i).name;
            Debug.Log(name);
            if (name == "CurrentLevelScene")
                canEvent = true;
        }
        if (gamepadEvent != null && canEvent)
            gamepadEvent();
        InputSystem.EnableDevice(Gamepad.current);
        if (Keyboard.current != null)
            InputSystem.DisableDevice(Keyboard.current);
        if (Mouse.current != null)
            InputSystem.DisableDevice(Mouse.current);

    }
    private void SwitchToKeyboard()
    {
        int count = SceneManager.sceneCount;
        bool canEvent = false;
        Debug.Log("count: " + count);
        for (int i = 0; i < count; i++)
        {
            string name = SceneManager.GetSceneAt(i).name;
            Debug.Log(name);
            if (name == "CurrentLevelScene")
                canEvent = true;
        }
        if (keyboardEvent != null && canEvent)
            keyboardEvent();
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Mouse.current);
        if (Gamepad.current != null)
            InputSystem.DisableDevice(Gamepad.current);

    }
}
