using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StateMachine.Dragon_Enemy;

public class DragonCallDeadEvent : MonoBehaviour
{
    public void PerfDead()
    {
        DragonStateMachine stateMachine = GetComponent<DragonStateMachine>();
        Enemy enemy = GetComponent<Enemy>();
        stateMachine.navAgent.enabled = false;
        stateMachine.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemy.ParticleDead();
        enemy.transform.GetChild(1).gameObject.SetActive(false);
        // enemy.GetComponent<Rigidbody>().isKinematic = true;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<Collider>().enabled = false;
        enemy.Drop();
        GetComponent<DragonStateMachine>().SetIsDieAnim(true);
    }

}
