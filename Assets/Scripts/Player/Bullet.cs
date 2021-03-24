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

    private void OnCollisionEnter(Collision other)
    {   
        // Debug.Log(other.gameObject.name);
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
            StartCoroutine(WaitToDestroy(.2f));
    }

    void Update()
    {
        if (Vector3.Distance(initialPos, transform.position) > GameManager.Instance.player.playerStats.Range)
            rb.constraints = RigidbodyConstraints.None;
    }
}
