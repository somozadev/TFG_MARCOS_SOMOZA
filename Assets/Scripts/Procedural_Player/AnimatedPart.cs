using UnityEngine;
[System.Serializable]
public class AnimatedPart
{
    public Transform transform;
    public Vector3 eulerRotationsYawDelta;
    public Vector3 eulerRotationsVelocity;

    public float rotationDamp;
    [HideInInspector] public Quaternion rotation;
    [HideInInspector] public Quaternion startRotation;
}
