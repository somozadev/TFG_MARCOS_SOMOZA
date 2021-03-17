using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    [Range(1,10)]
    [SerializeField] float waitDestroyTime = 4f;
    private void Awake()
    {
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
