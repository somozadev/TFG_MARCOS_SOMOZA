using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceChange : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text text;
    [SerializeField] bool listenGamepad, listenKeyboard;

    public void OnDeviceChanged()
    {
        StopTime();
        animator.SetBool("OnDisable", true);
        animator.SetBool("OnEnable", false);

    }
    private void Start()
    {
        GameManager.Instance.deviceController.gamepadEvent += SetUpTextGamepad;
        GameManager.Instance.deviceController.keyboardEvent += SetUpTextKeyboard;

    }
    void Update()
    {
        if (listenGamepad)
            ListenInput(Gamepad.current);
        if (listenKeyboard)
            ListenInput(Keyboard.current);
    }
    private void ListenInput(InputDevice device)
    {
        if (device != null)
        {
            if (device == Gamepad.current)
            {
                if (Gamepad.current.aButton.isPressed)
                {
                    ResumeTime();
                    animator.SetBool("OnEnable", false);
                    animator.SetBool("OnDisable", true);
                    listenGamepad = false;
                }
            }
            else
            {
                if (Keyboard.current.spaceKey.isPressed)
                {
                    ResumeTime();
                    animator.SetBool("OnEnable", false);
                    animator.SetBool("OnDisable", true);
                    listenKeyboard = false;
                }
            }
        }

    }
    private void SetUpTextGamepad()
    {
        if (Gamepad.current is UnityEngine.InputSystem.XInput.XInputController)
            text.text = "PRESS <sprite index=1> TO START";

        else if (Gamepad.current is UnityEngine.InputSystem.DualShock.DualShockGamepad)
            text.text = "PRESS <sprite index=2> TO START";
        animator.SetBool("OnDisable", false);
        animator.SetBool("OnEnable", true);
        listenGamepad = true;
        StopTime();

    }
    private void SetUpTextKeyboard()
    {
        text.text = "PRESS <sprite index=0> TO START";
        animator.SetBool("OnDisable", false);
        animator.SetBool("OnEnable", true);
        listenKeyboard = true;
        StopTime();

    }

    public void StopTime() { Debug.Log("TIMESTOP"); Time.timeScale = 0; }
    public void ResumeTime() { Debug.Log("TIMERESUME"); Time.timeScale = 1; }

    public void DisableObject() => animator.transform.GetChild(0).gameObject.gameObject.SetActive(false);
    public void EnableObject() => animator.transform.GetChild(0).gameObject.gameObject.SetActive(true);
}
