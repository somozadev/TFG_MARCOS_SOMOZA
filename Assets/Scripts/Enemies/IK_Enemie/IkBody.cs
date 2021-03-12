using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkBody : MonoBehaviour
{
    [SerializeField] public bool isHead;
    [SerializeField] public IkBody child;
    [SerializeField] public IkBody parent;
    [SerializeField] Target[] targets = new Target[2];
    [SerializeField]
    float legsHeighPercent
    {
        get
        {
            float a1 = targets[0].transform.localPosition.y;
            float a2 = targets[1].transform.localPosition.y;


            if (a1 > -1.25f && a1 <= 0)
            {
                a1 = targets[0].transform.localPosition.y - 1.25f;
            }
            else if (a1 > 0)
                a1 = targets[0].transform.localPosition.y + 1.25f;
            if (a2 > -1.25f && a2 <= 0)
            {
                a2 = targets[1].transform.localPosition.y - 1.25f;
            }
            else if (a2 > 0)
                a2 = targets[1].transform.localPosition.y + 1.25f;

            float res = (a1 + a2) / 2;
            return res;

        }
    }
    [SerializeField] private float baseY;
    [Range(2f, 10f)]
    [SerializeField] float speed = 2f;
    [SerializeField] LayerMask layer;
    RaycastHit hit;
    [SerializeField] float startDistance = -1.747552f;
    [SerializeField] float currentDistance;
    [SerializeField] float rayOffset = 5f;

    private void Start()
    {
        Debug.Log("0:" + targets[0].transform.localPosition.y);
        Debug.Log("1:" + targets[1].transform.localPosition.y);
        Debug.Log("B:" + transform.localPosition.y);
        Debug.LogError(legsHeighPercent);
        baseY = 0;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance * 2, Color.yellow);
            startDistance = -hit.distance;
        }
        currentDistance = -startDistance;

    }

    private void Update()
    {
        UpdateHeight();
        GetHeight();


    }


    public void MoveSelf(Transform followingBodyPart, bool forward)
    {
        Vector3 newPos = new Vector3();
        float T = 0;


        float distance = Vector3.Distance(followingBodyPart.position, transform.position);
        newPos = followingBodyPart.position;
        newPos.y = transform.position.y; Debug.Log(newPos);

        Debug.Log(newPos);
        T = Time.deltaTime * distance / 1f * speed;


        if (T > 0.5f)
            T = 0.5f;
        transform.position = Vector3.Slerp(transform.position, newPos, T);
        transform.rotation = Quaternion.Slerp(transform.rotation, followingBodyPart.rotation, T);

        if (forward)
        {
            if (child != null)
                child.MoveSelf(transform, forward);
        }
        else
        {
            if (parent != null)
                parent.MoveSelf(transform, forward);
        }
    }



    private void UpdateHeight()
    {

        // transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, (baseY + legsHeighPercent), transform.position.z), speed * Time.deltaTime);
        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, -currentDistance, transform.position.z), speed * Time.deltaTime);

    }
    private void GetHeight()
    {
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y + rayOffset, transform.position.z), Vector3.down, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawRay(transform.position, rayOffset * Vector3.down * hit.distance * 2, Color.red);
            currentDistance = Vector3.Distance(transform.position , hit.point);
        }
    }


 void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x,transform.position.y + rayOffset,transform.position.z), 0.1f);
    }


}
