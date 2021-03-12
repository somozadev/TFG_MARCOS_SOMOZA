using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private IkCharacter ikParent;
    public Side side;


    [SerializeField] float maxDistance = 1f;
    [SerializeField] float rayOffset = 5f;

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

    [Range(15, 50)]
    [SerializeField] float speed = 25f;
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


    //giving some errors => revisar
    private void Fix2Ground()
    {
        Vector3 from = new Vector3(movingTarget.position.x, movingTarget.position.y + rayOffset, movingTarget.position.z);

        // if (Physics.SphereCast(from, 0.6f, Vector3.down, out hit, Mathf.Infinity, layer))
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

    
    //maybe too slow... check with higher speed
    private void Move()
    {

        float timer = speed * Time.deltaTime;

        changeSide = false;
        lastPos = Vector3.Slerp(lastPos, movingTarget.position, timer) + ((new Vector3(0, 1f, 0) * curveStep.Evaluate(t)) * stepHeightMultiplier);

        t = Mathf.Lerp(t, 1f, timer);
        if (t >= .99999f)
        {
            // lastPos = movingTarget.position;
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
    private void Update()
    {
        if (Vector3.Distance(transform.position, asociatedEnd.position) > (maxDistance + 3f))
            Move(); Fix2Ground();
    }
    private void FixedUpdate()
    {
        transform.position = lastPos;
        // if (isLerping)
        //     Move();
        // if (canMove)
        // {
        if (canMove && (CheckOutOfRange() || isLerping))
            Move();

        else
            Fix2Ground();
        // }
        // else
        // {
        //     if (CheckOutOfRange())
        //         Move();
        //     Fix2Ground();
        // }

        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 from = new Vector3(movingTarget.position.x, movingTarget.position.y + rayOffset, movingTarget.position.z);
        Debug.DrawRay(from, Vector3.down * hit.distance, Color.yellow);
        Gizmos.DrawWireSphere(from, 0.1f);
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