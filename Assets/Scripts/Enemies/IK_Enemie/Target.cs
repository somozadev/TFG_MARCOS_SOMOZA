﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private IkCharacter ikParent;
    public Side side;


    [SerializeField] float maxDistance = 1f;

    [SerializeField] Vector3 lastPos;
    [SerializeField] Vector3 defaultPos;
    [SerializeField] bool canMove = false;
    [SerializeField] private bool isLerping = false;
    public bool changeSide = false;

    [SerializeField] Transform asociatedEnd;
    [SerializeField] Transform movingTarget;

    [SerializeField] LayerMask layer;
    RaycastHit hit;

    [SerializeField] public float CheckDistance { get { return Vector3.Distance(movingTarget.position, transform.position); } }

    [Range(1, 8)]
    [SerializeField] float speed = 2f;
    private float t = 0f;
    [SerializeField] float stepHeightMultiplier = 0.025f;
    [SerializeField] private float animationTime;

    [SerializeField] AnimationCurve curveStep;


    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private void Awake()
    {
        ikParent = GetComponentInParent<IkCharacter>();
    }
    private void Start()
    {
        lastPos = transform.position;
        defaultPos = transform.localPosition;
    }


    private void Fix2Ground()
    {
        Vector3 from = new Vector3(movingTarget.position.x, movingTarget.position.y + 1f, movingTarget.position.z);
        if (Physics.Raycast(from, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawRay(from, Vector3.down * hit.distance, Color.yellow);

            movingTarget.position = hit.point;
        }
    }
    private bool CheckOutOfRange()
    {
        bool outOfRange = false;

        if (CheckDistance >= maxDistance)
            outOfRange = isLerping = true;


        return outOfRange;

    }

    private void Move()
    {

        changeSide = false;
        lastPos = Vector3.Lerp(lastPos, movingTarget.position, speed * Time.deltaTime) + ((new Vector3(0, 1f, 0) * curveStep.Evaluate(t)) * stepHeightMultiplier);

        t = Mathf.Lerp(t, 1f, speed * Time.deltaTime);
        if (t >= .99f)
        {
            changeSide = true;
            isLerping = false;
            canMove = false;
            animationTime = 0;
            t = 0;

            ikParent.MoveNextLeg(ikParent.legs.IndexOf(this));


        }

        // Debug.Log("t:" + t);
        // Debug.Log("t_evaluate:" + curveStep.Evaluate(t));
    }
    private void FixedUpdate()
    {
        transform.position = lastPos;
        if (canMove)
        {
            if (CheckOutOfRange() || isLerping)
                Move();

            else
                Fix2Ground();
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(hit.point, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(movingTarget.position, .2f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .2f);
        Gizmos.DrawWireSphere(transform.position, maxDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, movingTarget.position);
    }
}


public enum Side
{
    LEFT, RIGHT
}