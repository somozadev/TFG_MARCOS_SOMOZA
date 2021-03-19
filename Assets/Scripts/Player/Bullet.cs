using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    [Range(1,10)]
    [SerializeField] float waitDestroyTime = 4f;
    [SerializeField] ParticleSystemRenderer topFire;
    private void Awake()
    {   
        topFire.material.SetVector("_Seed",new Vector4(Random.Range(0,10),Random.Range(0,10),0,0));
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(waitDestroyTime);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
