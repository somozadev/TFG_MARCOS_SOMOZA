using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    public bool legGrounded = false;
    public Vector3 stepPoint;
    public Vector3 stepNormal;

    public Vector3 optimalRestingPosition = Vector3.forward;
    public Vector3 restingPosition { get { return transform.TransformPoint(optimalRestingPosition); } }
    public Vector3 worldVelocity = Vector3.right;

    public Vector3 desiredPosition { get { return restingPosition + worldVelocity + (Random.insideUnitSphere * placementRandomization); } }

    public Vector3 worldTarget = Vector3.zero;
    public Transform ikTarget;
    public Transform ikPoleTarget;

    public float placementRandomization = 0;

    public bool autoStep = true;

    public LayerMask solidLayer;
    public float stepRadius = 0.25f;
    public AnimationCurve stepHeightCurve;
    public float stepHeightMultiplier = 0.25f;
    public float stepCooldown = 1f;
    public float stepDuration = 0.5f;
    public float stepOffset;
    public float lastStep = 0;

    public float percent { get { return Mathf.Clamp01((Time.time - lastStep) / stepDuration); } }

    private void Start()
    {
        worldVelocity = Vector3.zero;
        lastStep = Time.time + stepCooldown * stepOffset;
        ikTarget.position = restingPosition;
        Step();
    }



    private void Step()
    {
        stepPoint = worldTarget = AdjustPosition(desiredPosition);
        lastStep = Time.time;
    }

    private void Update()
    {
        UpdateIkTarget();
        if (Time.time > lastStep + stepCooldown && autoStep)
            Step();
    }

    public void UpdateIkTarget()
    {
        stepPoint = AdjustPosition(worldTarget + worldVelocity);
        ikTarget.position = Vector3.Lerp(ikTarget.position, stepPoint, percent) + stepNormal * stepHeightCurve.Evaluate(percent) * stepHeightMultiplier;
    }
    public void MoveVelocity(Vector3 newVel)
    {
        worldVelocity = Vector3.Lerp(worldVelocity, newVel, 1f - percent);
    }
    public Vector3 AdjustPosition(Vector3 position)
    {
        Vector3 dir = position - ikPoleTarget.position;
        RaycastHit hit;
        if (Physics.SphereCast(ikPoleTarget.position, stepRadius, dir, out hit, dir.magnitude * 2f, solidLayer))
        {
            Debug.DrawLine(ikPoleTarget.position, hit.point, Color.green, 0f);
            position = hit.point;
            stepNormal = hit.normal;
            legGrounded = true;
        }
        else
        {
            Debug.DrawLine(ikPoleTarget.position, restingPosition, Color.red, 0f);
            position = restingPosition;
            stepNormal = Vector3.zero;
            legGrounded = false;
        }
        return position;
    }
}
