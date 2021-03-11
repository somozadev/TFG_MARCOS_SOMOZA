using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CentipedeController : IkCharacter
{
    [SerializeField] EnemyState state; 
    [SerializeField] EnemyType type; 
    [Range(3f, 9f)]
    [SerializeField] float speed = 1f;
    [Range(10f, 30f)]
    [SerializeField] float angularSpeed = 15f;
    [SerializeField] GameObject head;
    public float rotationPercentaje = 30f;
    public bool positive;

    void Awake()
    {
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
            positive = false;
            head.GetComponent<IkBody>().RotateSelf(0, -angularSpeed * Time.deltaTime * 4, 0, rotationPercentaje, positive);
            // head.transform.Rotate(0,-angularSpeed * Time.deltaTime * 4, 0);
        }
        if (Keyboard.current.eKey.isPressed)
        {
            positive = true;
            head.GetComponent<IkBody>().RotateSelf(0, angularSpeed * Time.deltaTime * 4, 0, rotationPercentaje, positive);
            // head.transform.Rotate(0, angularSpeed * Time.deltaTime * 4, 0);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            head.transform.Translate(-transform.right * speed * Time.deltaTime);
            // head.transform.Translate(-speed * Time.deltaTime, 0, 0);s
        }
        if (Keyboard.current.dKey.isPressed)
        {
            head.transform.Translate(transform.right * speed * Time.deltaTime);
        }
        if (Keyboard.current.wKey.isPressed)
        {
            head.transform.Translate(transform.forward * speed * Time.deltaTime);
            // head.transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            head.transform.Translate(-transform.forward * speed * Time.deltaTime);
            // head.transform.Translate(0, 0, speed * Time.deltaTime);
        }

    }
}
