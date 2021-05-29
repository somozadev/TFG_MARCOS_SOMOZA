using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCallDeadEvent : MonoBehaviour
{
    public void PerfDead()
    {
        GetComponent<Enemy>().ParticleDead();
        GetComponent<Enemy>().transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Enemy>().GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Enemy>().GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Enemy>().GetComponent<Collider>().enabled = false;
        GetComponent<Enemy>().Drop();
        // StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);

    }

}
