using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float dmg;
    public float waitDestroyTime = 4f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().playerStats.RecieveDamage(dmg);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(WaitToDestroy(waitDestroyTime));
    }

    private IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
