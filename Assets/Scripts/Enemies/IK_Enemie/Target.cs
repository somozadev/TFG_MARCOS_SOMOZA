using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Vector3 GotoPos;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(GotoPos, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .2f);
    }
}
