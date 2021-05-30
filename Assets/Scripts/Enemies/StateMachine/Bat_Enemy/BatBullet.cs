using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBullet : MonoBehaviour
{
    public float dmg;
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().playerStats.RecieveDamage(dmg);
            Destroy(gameObject);
        }
    }
}
