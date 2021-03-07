using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CentipedeController : IkCharacter
{
    [Range(0f, 3f)]
    [SerializeField] float speed = 1f;
    [Range(10f, 30f)]
    [SerializeField] float angularSpeed = 15f;
    [SerializeField] GameObject head;
    override public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            head.transform.Rotate(0,-angularSpeed * Time.deltaTime * 4, 0);
        }
        if (Keyboard.current.eKey.isPressed)
        {
            head.transform.Rotate(0, angularSpeed * Time.deltaTime * 4, 0);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            head.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            head.transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Keyboard.current.wKey.isPressed)
        {
            head.transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            head.transform.Translate(0, 0, speed * Time.deltaTime);
        }

    }
}
