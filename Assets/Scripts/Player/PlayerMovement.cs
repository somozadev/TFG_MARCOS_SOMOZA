﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    [Header("Interact")]
    [SerializeField] private bool isInteracting;
    [Header("Movement")]
    private int speed;
    [SerializeField] private bool isMoving;
    private Vector3 moveDirection, forward, right;
    private Rigidbody rb;
    private Vector2 rawInput;
    [Header("Attack")]
    [SerializeField] private bool isAttacking;
    private float attSpeed;
    private float attRate;

    public PlayerInput PlayerInput { get { return playerInput; } }
    public bool IsInteracting { get { return isInteracting; } }

    private void Awake()
    {
        speed = Convert.ToInt32(GetComponent<Player>().playerStats.Spd * 1000);
        attRate = GetComponent<Player>().playerStats.Attrate;
        attSpeed = GetComponent<Player>().playerStats.Attspd;
        playerInput = GetComponentInChildren<PlayerInput>();
        rb = GetComponent<Rigidbody>();

    }

    void Start()
    {
        SetupInitialIsoDirection();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            Move(rawInput);
        if (isAttacking)
            Attack();
    }


    #region INPUT_SEND_MESSAGES
    private void OnAttack(InputValue value)
    {
        isAttacking = !isAttacking;
    }
    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        isMoving = rawInput == Vector2.zero ? false : true;

        moveDirection = new Vector3(rawInput.x, 0, rawInput.y).normalized;

    }
    private void OnInteract(InputValue value)
    {
        isInteracting = !isInteracting;
        if (isInteracting)
            Interact();
    }
    #endregion

    #region INPUT_METHODS
    private void Attack()
    {

    }
    private void Interact()
    {
            GetComponentInChildren<PlayerInteractor>().interacting = false;
        
    }

    private void Move(Vector2 input)
    {
        if (input.magnitude >= 0.2f)
        {
            Vector3 rightMovement = right * speed * Time.deltaTime * moveDirection.x;
            Vector3 upMovement = forward * speed * Time.deltaTime * moveDirection.z;
            Vector3 finalDirection = Vector3.Normalize(rightMovement + upMovement);
            transform.forward = finalDirection;

            rb.AddForce(rb.position + (rightMovement + upMovement));
            // rb.MovePosition(rb.position + (rightMovement + upMovement));
        }
    }



    private void SetupInitialIsoDirection()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    #endregion

}
