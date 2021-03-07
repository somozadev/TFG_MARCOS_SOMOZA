using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkBody : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    RaycastHit hit;
    [SerializeField] float startDistance = -1.747552f;
    [Range(2f, 10f)]
    [SerializeField] float speed = 2f;
    [SerializeField] float currentDistance;

    private void Start()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance * 2, Color.yellow);
            startDistance = -hit.distance;
        }
        // startDistance = -1.747552f;

    }

    private void FixedUpdate()
    {
        GetHeight();
        UpdateHeight();
    }

    private void UpdateHeight()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -currentDistance, transform.position.z), speed * Time.deltaTime);
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
