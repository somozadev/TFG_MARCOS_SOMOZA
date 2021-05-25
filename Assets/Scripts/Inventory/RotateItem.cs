using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    [SerializeField] float speed = 60f;
    private void Start() => StartCoroutine(RotateIt());


    IEnumerator RotateIt()
    {
        while (true)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
            yield return new WaitForEndOfFrame();
        }
    }
}
