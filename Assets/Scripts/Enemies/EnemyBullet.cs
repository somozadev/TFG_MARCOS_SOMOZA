using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float dmg;
    public float waitDestroyTime = 4f;
    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().playerStats.RecieveDamage(dmg);
            Destroy(gameObject);
        }
    }
    public virtual void Start()
    {
        StartCoroutine(WaitToDestroy(waitDestroyTime));
    }

    public IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
