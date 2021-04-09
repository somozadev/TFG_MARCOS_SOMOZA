using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class ToolCameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public EditorTool.RoomEditorTool tool;
    public float speed = 30f;
    Vector3 p = new Vector3();

    void Start() => offset = target.position; 

    private void Update()
    {
        target.position = offset;
        transform.LookAt(target);
        if (Keyboard.current.lKey.isPressed)
            speed = 50f;

        if (Vector3.Distance(transform.position, target.position) > 5)
        {
            if (Keyboard.current.wKey.isPressed)
            {
                p = new Vector3(0, 0, 1 * Time.deltaTime * speed);
                transform.Translate(p);
            }
        }
        if (Vector3.Distance(transform.position, target.position) < 20)
        {
            if (Keyboard.current.sKey.isPressed)
            {
                p = new Vector3(0, 0, -1 * Time.deltaTime * speed);
                transform.Translate(p);
            }
        }
        if (Keyboard.current.aKey.isPressed)
        {
            p = new Vector3(-1 * Time.deltaTime * speed, 0, 0);
            transform.Translate(p);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            p = new Vector3(1 * Time.deltaTime * speed, 0, 0);
            transform.Translate(p);
        }

    }

    public void MoveOffsetTop(float amount) => offset = new Vector3(offset.x, offset.y, offset.z + amount);
    public void MoveOffsetBot(float amount) => offset = new Vector3(offset.x, offset.y, offset.z - amount);
    public void MoveOffsetRight(float amount) => offset = new Vector3(offset.x + amount, offset.y, offset.z);
    public void MoveOffsetLeft(float amount) => offset = new Vector3(offset.x - amount, offset.y, offset.z);

}
