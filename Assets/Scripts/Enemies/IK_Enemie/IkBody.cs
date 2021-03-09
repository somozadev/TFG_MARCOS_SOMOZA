using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkBody : MonoBehaviour
{
    [SerializeField] public bool isHead;
    [SerializeField] IkBody child;
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
        Debug.DrawRay(transform.position, child.transform.position, Color.green);


    }


    public void RotateSelf(float x, float y, float z, float rotationPercentaje, bool positive)
    {
        if (isHead)
            transform.Rotate(x, y, z);
        else
        {
            if (positive)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(x, -(y * rotationPercentaje) / 100, z), Time.deltaTime * 15);
            else
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(x, (y * rotationPercentaje) / 100, z), Time.deltaTime * 15);
        }
        if (child != null)
        {
            child.RotateSelf(x, y, z, rotationPercentaje, positive);
        }
    }





    private void UpdateHeight()
    {

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (baseY + legsHeighPercent) , transform.position.z), speed * Time.deltaTime);
        // transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -currentDistance, transform.position.z), speed * Time.deltaTime);

    }
    private void GetHeight()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance * 2, Color.red);
            currentDistance = hit.distance;
        }
    }
}
