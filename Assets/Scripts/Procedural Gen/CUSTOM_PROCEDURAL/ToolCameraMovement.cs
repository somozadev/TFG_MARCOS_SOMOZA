using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class ToolCameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 startPos;
    public bool lockOffsetL;
    public bool lockOffsetR;
    public bool lockOffsetT;
    public bool lockOffsetB;

    public EditorTool.RoomEditorTool tool;
    public float speed = 30f;
    Vector3 p = new Vector3();

    void Start() => startPos = offset = target.position;


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
        if (Keyboard.current.upArrowKey.isPressed)
            if (!lockOffsetT)
                MoveOffsetTop(0.2f);
        if (Keyboard.current.downArrowKey.isPressed)
            if (!lockOffsetB)
                MoveOffsetBot(0.2f);
        if (Keyboard.current.leftArrowKey.isPressed)
            if (!lockOffsetL)
                MoveOffsetLeft(0.2f);
        if (Keyboard.current.rightArrowKey.isPressed)
            if (!lockOffsetR)
                MoveOffsetRight(0.2f);

        LimitTargetToGrid();

    }

    public void LimitTargetToGrid()
    {
        if (target.position.z > tool.grid.z * tool.grid.offset)
        {
            target.position = new Vector3(target.position.x, target.position.y, (tool.grid.z * tool.grid.offset) - (tool.grid.offset / 2));
            lockOffsetT = true;
        }
        if (target.position.x > tool.grid.x * tool.grid.offset)
        {
            target.position = new Vector3((tool.grid.x * tool.grid.offset) - (tool.grid.offset / 2), target.position.y, target.position.z);
            lockOffsetR = true;
        }
        if (target.position.z < 0)
        {
            target.position = new Vector3(target.position.x, target.position.y, -2);
            lockOffsetB = true;
        }
        if (target.position.x < 0)
        {
            target.position = new Vector3(-2, target.position.y, target.position.z);
            lockOffsetL = true;
        }
    }

    public void MoveOffsetTop(float amount) { offset = new Vector3(offset.x, offset.y, offset.z + amount); lockOffsetB = false; }
    public void MoveOffsetBot(float amount) { offset = new Vector3(offset.x, offset.y, offset.z - amount); lockOffsetT = false; }
    public void MoveOffsetRight(float amount) { offset = new Vector3(offset.x + amount, offset.y, offset.z); lockOffsetL = false; }
    public void MoveOffsetLeft(float amount) { offset = new Vector3(offset.x - amount, offset.y, offset.z); lockOffsetR = false; }
    public void MoveOffsetCenter() => offset = startPos;

}
