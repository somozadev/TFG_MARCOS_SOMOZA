using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DeviceController : MonoBehaviour
{
    public DevicesDictionaryCompound currentDevices;

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
        if (Gamepad.current != null && Gamepad.current.enabled)
            SwitchToGamepad();

        else
            SwitchToKeyboard();
        InitCurrentDevices();
    }



    private void SwitchToGamepad()
    {
        InputSystem.EnableDevice(Gamepad.current);
        InputSystem.DisableDevice(Keyboard.current);
        InputSystem.DisableDevice(Mouse.current);
    }
    private void SwitchToKeyboard()
    {
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Mouse.current);
        if (Gamepad.current != null)
            InputSystem.DisableDevice(Gamepad.current);
    }
}
