using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PressToStart : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    [SerializeField] DevicesDictionaryCompound currentDevices;

    private void Start()
    {
        InitCurrentDevices();
    }


    private void InitCurrentDevices()
    {
        if (Gamepad.current != null)
            currentDevices = new DevicesDictionaryCompound(Keyboard.current.name, Keyboard.current.enabled, Mouse.current.name, Mouse.current.enabled, Gamepad.current.name, Gamepad.current.enabled);
        else
            currentDevices = new DevicesDictionaryCompound(Keyboard.current.name, Keyboard.current.enabled, Mouse.current.name, Mouse.current.enabled, "disconnected Gamepad", false);


    }
    private void UpdateCurrentDevices()
    {
        InitCurrentDevices();
    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.enabled)
            SwitchToGamepad();

        else
            StayWithKeyboard();
        UpdateCurrentDevices();
    }


    private void SwitchToGamepad()
    {
        text.text = "PRESS <color=#FFFF00>A</color> TO START";
        InputSystem.EnableDevice(Gamepad.current);
        InputSystem.DisableDevice(Keyboard.current);
        InputSystem.DisableDevice(Mouse.current);
        ListenInput(Gamepad.current);
    }
    private void StayWithKeyboard()
    {
        text.text = "PRESS <color=#FFFF00>SPACEBAR</color> TO START";
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Mouse.current);
        if (Gamepad.current != null)
            InputSystem.DisableDevice(Gamepad.current);

        ListenInput(Keyboard.current);
    }

    private void ListenInput(InputDevice device)
    {
        if (device == Gamepad.current)
        {
            if (Gamepad.current.aButton.isPressed)
            {
                Debug.LogWarning("A BUTTON PRESSED STARTING GAME");
                //TRIGGER START SCENE}
            }
        }
        else
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                Debug.LogWarning("SPACE BUTTON PRESSED STARTING GAME");
                //TRIGGER START SCENE}
            }

        }

    }
}

[System.Serializable]
public class DevicesDictionary
{
    public string name;
    public bool enabled;

    public DevicesDictionary(string name, bool enabled)
    {
        this.name = name;
        this.enabled = enabled;
    }
}

[System.Serializable]
public class DevicesDictionaryCompound
{
    public List<DevicesDictionary> devices;

    public DevicesDictionaryCompound(string name1, bool value1, string name2, bool value2, string name3, bool value3)
    {
        this.devices = new List<DevicesDictionary>();
        this.devices.Add(new DevicesDictionary(name1, value1));
        this.devices.Add(new DevicesDictionary(name2, value2));
        this.devices.Add(new DevicesDictionary(name3, value3));
    }
}


//playerInput.SwitchCurrentActionMap("actionMapName");