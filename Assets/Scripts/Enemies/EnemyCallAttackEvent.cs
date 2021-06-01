using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallAttackEvent : MonoBehaviour
{
    [SerializeField] Transform hitBone;
    public void DoDamage()
    {
        int layerMask = 0<<13;// = 13;//LayerMask.GetMask("Player"); //playerLayerMask
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
            GetComponent<Enemy>().conditions.isAttackRange = false;
            GetComponent<Enemy>().conditions.isChasing = true;
            GetComponent<Enemy>().conditions.isAttacking = false;
            Debug.Log("Did not Hit");
        }
    }

}
