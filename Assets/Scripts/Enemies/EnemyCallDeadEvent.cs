using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCallDeadEvent : MonoBehaviour

{
    public virtual void PerfDead()
    {   
        Enemy enemy = GetComponent<Enemy>();
        enemy.ParticleDead();
        enemy.transform.GetChild(1).gameObject.SetActive(false);
        enemy.GetComponent<Rigidbody>().isKinematic = true;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<Collider>().enabled = false;
        enemy.Drop();
        GameManager.Instance.dataController.AddAnotherEnemiesKilled();
        if(GetComponentInParent<Room>())
            GetComponentInParent<Room>().EnemyDiedCall(gameObject);
    }

}
