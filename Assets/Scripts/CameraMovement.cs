using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Target")]
    [SerializeField] private GameObject player;

    [Header("Camera Settings")]
    [Tooltip("Camera offset from the center focus")]
    public Vector3 offset;
    [Range(0.01f, 1.0f)]
    [Tooltip("0 means no smooth in camera follow movement. 1 full smooth")]
    public float SmoothFactor = 0.125f;

    [Header("Zoom Settings")]
    [Tooltip("Maximum withing the limits for the zoom")]
    public float maxZoom = 40f;
    [Tooltip("Minimum withing the limits for the zoom")]
    public float minZoom = 10f;
    [Tooltip("Farest positioning in zoom limits")]
    public float zoomLimit = 50f;

    private Vector3 vel;


    private void FixedUpdate()
    {
        Move();
        Zoom();
    }




    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, zoomLimit);
        GetComponentInChildren<Camera>().fieldOfView = Mathf.Lerp(GetComponentInChildren<Camera>().fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref vel, SmoothFactor);
    }








}



