using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CentipedeController : IkCharacter
{
    public bool isMoving;
    public bool isForward;
    public bool isBackward;
    public bool isRotLeft;
    public bool isRotRight;
    public Transform TARGET;


    [SerializeField] NavMeshAgent agent;

    [SerializeField] EnemyState state;
    [SerializeField] public IkBody[] bodies;
    [SerializeField] EnemyType type;
    [Range(3f, 9f)]
    [SerializeField] float speed = 1f;
    [Range(10f, 30f)]
    [SerializeField] float angularSpeed = 15f;
    public GameObject head;
    public GameObject tail;
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


    public void Move() => isMoving = true;
    public void Stop() => isMoving = false;
    public void Forward() { isForward = forward = true; isBackward = false; }
    public void Backward() { isForward = forward = false; isBackward = true; }
    public void RotLeft() { isRotLeft = true; isRotRight = false; }
    public void RotRight() { isRotLeft = false; isRotRight = true; }
    public void NoRot() { isRotLeft = false; isRotRight = false; }

    public void MoveHeadForward()
    {
        forward = true;
        head.transform.Translate(transform.forward * speed * Time.deltaTime);
        head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);
    }
    public void MoveHeadBackward()
    {
        forward = false;
        tail.transform.Translate(-transform.forward * speed * Time.deltaTime);
        tail.GetComponent<IkBody>().parent.MoveSelf(tail.transform, forward);
    }
    public void RotateLeft()
    {
        if (isMoving == true)
        {
            if (forward)
                head.transform.Rotate(0, -angularSpeed * Time.deltaTime * 4, 0);
            else
                tail.transform.Rotate(0, -angularSpeed * Time.deltaTime * 4, 0);
        }
    }
    public void RotateRight()
    {
        if (isMoving == true)
        {
            if (forward)
                head.transform.Rotate(0, angularSpeed * Time.deltaTime * 4, 0);
            else
                tail.transform.Rotate(0, angularSpeed * Time.deltaTime * 4, 0);
        }
    }

    public bool FollowPlayer = false;
    public bool UnFollowPlayer = false;
    public bool OrbitPlayer = false;
    public void MoveToPlayer()
    {
        FollowPlayer = true;
        UnFollowPlayer = false;
    }
    public void RunFromPlayer()
    {
        FollowPlayer = false;
        UnFollowPlayer = true;
    }
    private void FollowState()
    {
        rotOnce = true;
        forward = true;
        // head.transform.position = Vector3.MoveTowards(head.transform.position, TARGET.position, Time.deltaTime * speed);
        Vector3 look = new Vector3(TARGET.position.x, head.transform.position.y, TARGET.position.z);

        head.transform.LookAt(look, head.transform.up);
        head.transform.Translate(-1 * head.transform.forward.normalized * speed * Time.deltaTime);

        head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);

    }
    private bool rotOnce = true;
    private void OrbitState()
    {
        if (rotOnce)
        {
            head.transform.Rotate(0, 90, 0);
            rotOnce = false;
        }
        forward = true;

        head.transform.RotateAround(TARGET.transform.position, Vector3.up, 20 * Time.deltaTime);
        head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);
    }
    private void UnFollowState()
    {
        forward = true;
        // head.transform.position = Vector3.MoveTowards(head.transform.position, TARGET.position, Time.deltaTime * speed);
        Vector3 look = new Vector3(TARGET.position.x, head.transform.position.y, TARGET.position.z);

        Debug.Log(Vector3.Distance(head.transform.position, TARGET.position));


        head.transform.LookAt(look, head.transform.up);
        head.transform.Translate(head.transform.forward * speed * Time.deltaTime);

        head.GetComponent<IkBody>().child.MoveSelf(head.transform, forward);
    }


    void Update()
    {
        if (Vector3.Distance(head.transform.position, TARGET.position) >= 15)
        { FollowPlayer = true; OrbitPlayer = false; }
        else
        { OrbitPlayer = true; FollowPlayer = false; }

        if (OrbitPlayer)
        {
            OrbitState();
        }
        if (FollowPlayer)
        {
            FollowState();
        }
        if (UnFollowPlayer)
        {
            UnFollowState();
        }
        if (isMoving)
        {
            if (isForward)
            {
                MoveHeadForward();
            }
            else if (isBackward)
            {
                MoveHeadBackward();
            }

            if (isRotLeft)
                RotateLeft();
            else if (isRotRight)
                RotateRight();
        }

    }
}
