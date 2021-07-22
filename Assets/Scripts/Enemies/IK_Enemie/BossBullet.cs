using System.Collections;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float dmg;
    public float waitDestroyTime = 24f;
    public Vector2 velocity;
    public float speed;
    public float rotation;

    public virtual void OnTriggerEnter(Collider other)
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
        transform.rotation = Quaternion.Euler(0, rotation, 0);
        if (GameManager.Instance != null)
            GameManager.Instance.soundManager.Play("BulletWind");
        StartCoroutine(WaitToDestroy(waitDestroyTime));
    }

    private void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }
    public IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
