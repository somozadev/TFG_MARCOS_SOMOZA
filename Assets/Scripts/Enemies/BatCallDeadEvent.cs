using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StateMachine.Bat_Enemy;

public class BatCallDeadEvent : MonoBehaviour
{
    public void PerfDead()
    {   
        Enemy enemy = GetComponent<Enemy>();
        enemy.ParticleDead();
        enemy.transform.GetChild(1).gameObject.SetActive(false);
        enemy.GetComponent<Rigidbody>().isKinematic = true;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<Collider>().enabled = false;
        enemy.Drop();
        GetComponent<BatStateMachine>().SetIsDieAnim(true);
    }

}
