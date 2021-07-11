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
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            StartCoroutine(WaitToDestroy(0.5f));
        }
    }
    public virtual void Start()
    {
        GameManager.Instance.soundManager.Play("BulletWind");
        StartCoroutine(WaitToDestroy(waitDestroyTime));
    }

    public IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
