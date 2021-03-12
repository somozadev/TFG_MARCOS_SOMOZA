using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CentipedeController : IkCharacter
{
    [SerializeField] bool isMoving;

    [SerializeField] EnemyState state;
    [SerializeField] public IkBody[] bodies;
    [SerializeField] EnemyType type;
    [Range(3f, 9f)]
    [SerializeField] float speed = 1f;
    [Range(10f, 30f)]
    [SerializeField] float angularSpeed = 15f;
    [SerializeField] GameObject head;
    [SerializeField] GameObject tail;
    public float rotationPercentaje = 30f;
    public bool forward;

    void Awake()
    {
        bodies = GetComponentsInChildren<IkBody>();
        type = EnemyType.CENTIPEDE;
        state = EnemyState.IDLE;
    }
    override public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            if (isMoving == true)
            {
                if (forward)
                    head.transform.Rotate(0, -angularSpeed * Time.deltaTime * 4, 0);
                else
                    tail.transform.Rotate(0, -angularSpeed * Time.deltaTime * 4, 0);
            }

        }
        if (Keyboard.current.eKey.isPressed)
        {
            if (isMoving == true)
            {
                if (forward)
                    head.transform.Rotate(0, angularSpeed * Time.deltaTime * 4, 0);
                else
                    tail.transform.Rotate(0, angularSpeed * Time.deltaTime * 4, 0);
            }
        }
        // if (Keyboard.current.aKey.isPressed)
        // {
        //     isMoving = true;
        //     forward = true;
        //     head.transform.Translate(-transform.right * speed * Time.deltaTime);
        //     head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);
        // }
        // if (Keyboard.current.dKey.isPressed)
        // {
        //     isMoving = true;
        //     forward = true;
        //     head.transform.Translate(transform.right * speed * Time.deltaTime);
        //     head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);
        // }
        isMoving = false;
        if (Keyboard.current.wKey.isPressed)
        {
            isMoving = true;
            forward = true;
            head.transform.Translate(transform.forward * speed * Time.deltaTime);
            head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            isMoving = true;
            forward = false;
            tail.transform.Translate(-transform.forward * speed * Time.deltaTime);
            tail.GetComponent<IkBody>().parent.MoveSelf(tail.transform, forward);
        }

    }
}
