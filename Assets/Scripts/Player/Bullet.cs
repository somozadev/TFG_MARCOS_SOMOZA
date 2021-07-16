using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    [Range(1, 10)]
    [SerializeField] float waitDestroyTime = 4f;
    [SerializeField] ParticleSystemRenderer topFire;
    private Vector3 initialPos;
    private void Awake()
    {
        tag = "Bullet";
        initialPos = transform.position;
        topFire.material.SetVector("_Seed", new Vector4(Random.Range(0, 10), Random.Range(0, 10), 0, 0));
        rb = GetComponent<Rigidbody>();
        transform.localScale = Vector3.one * GameManager.Instance.player.extraStats.ShotsSize;
    }
    void Start()
    {
        StartCoroutine(WaitToDestroy(waitDestroyTime));
    }

    private IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.player.playerMovement.Bullets.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log(other.gameObject.name);
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
            StartCoroutine(WaitToDestroy(.2f));
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().RecieveDamage(GameManager.Instance.player.playerStats.Dmg);
            if (GameManager.Instance.player.extraStats.HpSteal)
            {
                GameManager.Instance.player.playerStats.CurrentHp += GameManager.Instance.player.extraStats.HpStealValue;
            }
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().RecieveDamage(GameManager.Instance.player.playerStats.Dmg);
            if (GameManager.Instance.player.extraStats.HpSteal)
            {
                GameManager.Instance.player.playerStats.CurrentHp += GameManager.Instance.player.extraStats.HpStealValue;
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Vector3.Distance(initialPos, transform.position) > GameManager.Instance.player.playerStats.Range)
            rb.constraints = RigidbodyConstraints.None;
    }
}
