using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PressToStart : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    [SerializeField] DevicesDictionaryCompound currentDevices;
    private DeviceController dc;
    private void Start()
    {
        dc = GameManager.Instance.deviceController;
        currentDevices = dc.currentDevices;
    }
    private void UpdateCurrentDevices()
    {
        dc.InitCurrentDevices();
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
        if (Gamepad.current is UnityEngine.InputSystem.XInput.XInputController)
            text.text = "PRESS <sprite index=1> TO START";

        else if (Gamepad.current is UnityEngine.InputSystem.DualShock.DualShockGamepad)
            text.text = "PRESS <sprite index=2> TO START";

        InputSystem.EnableDevice(Gamepad.current);
        InputSystem.DisableDevice(Keyboard.current);
        InputSystem.DisableDevice(Mouse.current);
        ListenInput(Gamepad.current);
    }
    private void StayWithKeyboard()
    {
        Debug.Log("KEYBOARD");
        text.text = "PRESS <sprite index=0> TO START";
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
                SceneController.Instance.LoadAdresseableScene(SceneName.SaveFileScene, true);
            }
        }
        else
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                Debug.LogWarning("SPACE BUTTON PRESSED STARTING GAME");
                //TRIGGER START SCENE}
                SceneController.Instance.LoadAdresseableScene(SceneName.SaveFileScene, true);
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

#region DEADZONE_TMP


/*
*   SOME TEXT ANIMATION -> works weird with images
*
    [SerializeField] Mesh textMesh;
    [SerializeField] Vector3[] textVertices;
    [SerializeField] AnimationCurve curve;
*
*   private void UpdateTextMesh()
    {
        text.ForceMeshUpdate();
        textMesh = text.mesh;
        textVertices = textMesh.vertices;

        ApplyWobblePerCharacter(textVertices, textMesh, text);
    }

    private void ApplyWobblePerCharacter(Vector3[] vertices, Mesh mesh, TMP_Text text)
    {
        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = text.textInfo.characterInfo[i];
            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[i] += offset;
            vertices[i + 1] += offset;
            vertices[i + 2] += offset;
            vertices[i + 3] += offset;
        }
        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
// float offsetY = VertexCurve.Evaluate((float)i / characterCount + loopCount / 50f) * CurveScale; // Random.Range(-0.25f, 0.25f);                    


    private void ApplyWobble(Vector3[] vertices, Mesh mesh, TMP_Text text)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            vertices[i] += offset;
        }
        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
    private Vector2 Wobble(float t) { return new Vector2(Mathf.Sin(t * 3.3f), Mathf.Cos(t * 2.2f)); }



*   
*   
*   
*   
*   
*   
*/

#endregion