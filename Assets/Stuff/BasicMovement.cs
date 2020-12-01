using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(Input.GetAxis("Horizontal") >= 0)
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
        } 
        if(Input.GetAxis("Horizontal") <= 0)
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime);
        }
         if(Input.GetAxis("Vertical") >= 0)
        {
            rb.AddForce(new Vector3(0,0,1) * speed * Time.deltaTime);
        }
         if(Input.GetAxis("Horizontal") >= 0)
        {
            rb.AddForce(new Vector3(0,0,-1) * speed * Time.deltaTime);
        }
    }
}
