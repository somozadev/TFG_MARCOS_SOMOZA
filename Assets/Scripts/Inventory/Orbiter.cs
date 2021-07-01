using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    [SerializeField] Transform centers;
    [SerializeField] Vector3 velocity;
    [Range(0.01f, 0.1f)]
    [SerializeField] float iterationFactor;
    [Range(0.01f, 0.1f)]
    [SerializeField] float friction;
    [SerializeField] float frictionConstant;


    public void Start()
    {
        if (this.centers == null)
        {
            GameObject obj = GameManager.Instance.player.gameObject;
            if (obj != null)
            {
                this.centers = obj.transform;
            }
        }
    }

    public void FixedUpdate()
    {
        
            GravityPull(this.centers);
            CheckVelocity();
            AddVelocity();
        
    }

    public void CheckVelocity()
    {
        if (Mathf.Abs(this.velocity.x) < float.Epsilon * 2f)
        {
            this.velocity.x = 0f;
        }
        if (Mathf.Abs(this.velocity.y) < float.Epsilon * 2f)
        {
            this.velocity.y = 0f;
        }
        if (Mathf.Abs(this.velocity.z) < float.Epsilon * 2f)
        {
            this.velocity.z = 0f;
        }
    }

    public void GravityPull(Transform gravitationalCenter)
    {
        Vector3 direction = this.transform.position - (gravitationalCenter.transform.position + new Vector3(0,1,0));
        if (direction.magnitude < (gravitationalCenter.localScale.x / 2f) + 0.7f)
        {
            this.velocity = Vector3.zero;
        }
        else
        {
            direction = direction.normalized * this.iterationFactor;
            this.velocity += direction;
        }

        if (gravitationalCenter != null)
        {
            this.velocity -= this.velocity.normalized * frictionConstant * frictionConstant;
        }
    }

    public void AddVelocity()
    {
        Vector3 pos = this.transform.position;
        pos -= this.velocity;
        this.transform.position = pos;
    }



}
