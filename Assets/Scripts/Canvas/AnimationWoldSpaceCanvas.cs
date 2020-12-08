using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationWoldSpaceCanvas : MonoBehaviour, IAnimable
{
    [Range(0, 100)]
    [SerializeField] int speed = 50;
    [SerializeField] bool open;
    [Range(0.1f, 1)]
    [SerializeField] private float duration;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private AnimationCurve curve;
    private Quaternion initialRotation;


    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        transform.position = new Vector3(transform.position.x, curve.Evaluate(Time.time % curve.length), transform.position.z);
    }



    #region IAnimable



    public float Duration { get { return duration; } set { duration = value; } }
    public Vector3 FinalScale { get { return finalScale; } set { finalScale = value; } }
    public bool Open { get { return open; } set { open = value; } }
    public void Activate()
    {
        open = true;
        gameObject.SetActive(true);
        initialRotation = transform.rotation;
        finalScale = Vector3.one;
        transform.localScale = Vector3.zero;
        StartCoroutine(LerpAnim());
    }

    public void Deactivate()
    {
        open = false;
        transform.localScale = Vector3.one;
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
        if (!open)
        {
            transform.rotation = initialRotation;
            gameObject.SetActive(false);
        }
    }
    #endregion
}
