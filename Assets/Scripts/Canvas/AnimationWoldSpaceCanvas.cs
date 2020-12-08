using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationWoldSpaceCanvas : MonoBehaviour, IAnimable
{
    [Range(0, 100)]
    [SerializeField] int speed = 50;
    [SerializeField] private AnimationCurve curve;
    private Quaternion initialRotation;


    private void OnEnable()
    {
        Activate();
        initialRotation = transform.rotation;
    }

    private void OnDisable()
    {
        Deactivate();
        transform.rotation = initialRotation;
    }


    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        transform.position = new Vector3(transform.position.x, curve.Evaluate(Time.time % curve.length), transform.position.z);
    }



    #region IAnimable
    public float duration { get; set; }
    public Vector3 finalScale { get; set; }
    public void Activate()
    {
        finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(LerpAnim());
    }

    public void Deactivate()
    {
        finalScale = Vector3.zero;
        StartCoroutine(LerpAnim());
    }

    public IEnumerator LerpAnim()
    {
        for (float t = 0; t < duration * 2; t += Time.deltaTime)
        {
            float p = Mathf.PingPong(t, duration) / duration;
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, p);
            yield return null;
        }
        transform.localScale = finalScale;
    }
    #endregion
}
