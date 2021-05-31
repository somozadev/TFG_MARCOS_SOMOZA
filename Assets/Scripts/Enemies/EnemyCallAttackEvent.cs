using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallAttackEvent : MonoBehaviour
{
    [SerializeField] Transform hitBone;
    public void DoDamage()
    {
        // hitCollider.enabled = true; 
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(hitBone.position, hitBone.forward, out hit, 4.5f, layerMask))
        {
            Debug.DrawRay(hitBone.position, hitBone.forward * hit.distance, Color.yellow);
            GetComponent<Enemy>().MakeDamage();
            Debug.Log("Did Hit");

        }
        else
        {
            Debug.DrawRay(hitBone.position, hitBone.forward * 1000, Color.white);
            GetComponent<Enemy>().conditions.isRange = false;
            GetComponent<Enemy>().conditions.isChasing = true;
            GetComponent<Enemy>().conditions.isAttacking = false;
            Debug.Log("Did not Hit");
        }
    }

}
