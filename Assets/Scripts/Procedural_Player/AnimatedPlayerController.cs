using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlayerController : MonoBehaviour
{
    public Transform visuals;
    [Header("Movement")]
    public float acceleration;
    public float maxVelocity;
    [Header("Animation")]
    public float visualsRotationDamp;
    public float yawDeltaMax;
    public float yawDeltaDamp;
    public AnimatedPart[] animatedParts;

    Rigidbody body;

    float yawDelta;
    Vector3 lastForward;

    private void Start()
    {   
        body = GetComponent<Rigidbody>();
        for(int i = 0; i < animatedParts.Length; i++)
        {
            animatedParts[i].startRotation = animatedParts[i].transform.localRotation;
        }
    }

    private void LateUpdate()
    {
        if (body.velocity.sqrMagnitude > float.Epsilon)
        {
            Quaternion lookRot = Quaternion.LookRotation(body.velocity, Vector3.up);
            visuals.rotation = Quaternion.Slerp(visuals.rotation, lookRot, Time.deltaTime * visualsRotationDamp);
        }

        float newYawDelta = Vector3.SignedAngle(lastForward, visuals.forward, Vector3.up) / Time.deltaTime;
        lastForward = visuals.forward;

        if (Mathf.Abs(newYawDelta) > yawDelta)
        {
            if (newYawDelta >= 0)
                newYawDelta = yawDeltaMax;
            else
                newYawDelta = -yawDeltaMax;
        }

        yawDelta = Mathf.Lerp(yawDelta, newYawDelta * 0.01f, Time.deltaTime * yawDeltaDamp);
        Vector3 localVelocity = visuals.InverseTransformDirection(body.velocity);
        localVelocity = new Vector3(localVelocity.z, localVelocity.x, localVelocity.x);

        for (int i = 0; i < animatedParts.Length; i++)
        {
            Vector3 newYawDeltaRot = animatedParts[i].eulerRotationsYawDelta * yawDelta;
            Vector3 newVelocityRot = Vector3.Scale(animatedParts[i].eulerRotationsVelocity, localVelocity);

            Quaternion newRot = Quaternion.Euler(newVelocityRot + newYawDeltaRot);
            animatedParts[i].rotation = Quaternion.Slerp(animatedParts[i].rotation, newRot, Time.deltaTime * animatedParts[i].rotationDamp);
            animatedParts[i].transform.localRotation = animatedParts[i].startRotation * animatedParts[i].rotation;

        }

    }



}
