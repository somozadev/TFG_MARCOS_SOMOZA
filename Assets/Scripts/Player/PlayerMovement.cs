using System;
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
    private Vector3 moveDirection, lookDirection, forward, right;
    private Rigidbody rb;
    [SerializeField] Vector2 rawInput;
    [SerializeField] Vector2 rawInputShooting;
    [Header("Attack")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool canAttack = true;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [Range(1, 5)]
    [SerializeField] float attSpeed;
    [Range(1, 10)]
    [SerializeField] float attRate;

    public PlayerInput PlayerInput { get { return playerInput; } }
    public bool IsInteracting { get { return isInteracting; } }
    public Rigidbody Rb { get { return rb; } }

    private void Awake()
    {
        speed = Convert.ToInt32(GetComponent<Player>().playerStats.Spd * 1000);
        attRate = GetComponent<Player>().playerStats.Attrate;
        attSpeed = GetComponent<Player>().playerStats.Attspd;
        playerInput = GetComponentInChildren<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        canAttack = true;
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
            LookAttack(rawInputShooting);
    }


    #region INPUT_SEND_MESSAGES
    // private void OnAttack(InputValue value)
    // {
    //     isAttacking = !isAttacking;
    // }
    private void OnAttackDir(InputValue value)
    {
        rawInputShooting = value.Get<Vector2>();
        isAttacking = rawInputShooting == Vector2.zero ? false : true;
        lookDirection = new Vector3(rawInputShooting.x, 0, rawInputShooting.y).normalized;
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

    private void OnPause(InputValue value)
    {
        bool pause = Convert.ToBoolean(value.Get<float>());
        Debug.Log(pause);
        if (pause)
        {
            if (!GameManager.Instance.ingameCanvasController.paused)
                GameManager.Instance.ingameCanvasController.Pause();
            else
                GameManager.Instance.ingameCanvasController.UnPause();
        }
    }


    #endregion

    #region INPUT_METHODS
    private void LookAttack(Vector2 rawInput)
    {
        if (rawInput.magnitude >= 0.2f)
        {
            Vector3 rightMovement = right * speed * Time.deltaTime * lookDirection.x;
            Vector3 upMovement = forward * speed * Time.deltaTime * lookDirection.z;
            Vector3 finalDirection = Vector3.Normalize(rightMovement + upMovement);
            transform.forward = finalDirection;
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(WaitToAttackLook(attRate / 10));
            }
        }
    }


    // private void Attack()
    // {
    //     if (canAttack)
    //     {
    //         canAttack = false;
    //         StartCoroutine(WaitToAttack(attRate / 10));
    //     }
    // }
    private IEnumerator WaitToAttackLook(float waitTime)
    {

        float radius = 0.1f;

        for (int i = 0; i < GameManager.Instance.player.extraStats.NumberOfShots; i++)
        {
            Vector3 randomPos = new Vector3(shootingPoint.position.x + UnityEngine.Random.Range(-radius, radius), shootingPoint.position.y + UnityEngine.Random.Range(-radius, radius), shootingPoint.position.z);
            GameObject bullet = GameObject.Instantiate(bulletPrefab, randomPos, Quaternion.identity);
            GameManager.Instance.soundManager.Play("PlayerShoot");
            GameManager.Instance.soundManager.Play("BulletWind");
            bullet.GetComponent<Bullet>().rb.AddForce(transform.forward * attSpeed, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(waitTime);
        canAttack = true;



    }
    // private IEnumerator WaitToAttack(float waitTime)
    // {
    //     float radius = 0.1f;

    //     for (int i = 0; i < GameManager.Instance.player.extraStats.NumberOfShots; i++)
    //     {
    //         Vector3 randomPos = new Vector3(shootingPoint.position.x + UnityEngine.Random.Range(-radius, radius), shootingPoint.position.y + UnityEngine.Random.Range(-radius, radius), shootingPoint.position.z);
    //         GameObject bullet = GameObject.Instantiate(bulletPrefab, randomPos, Quaternion.identity);
    //         bullet.GetComponent<Bullet>().rb.AddForce(transform.forward * attSpeed, ForceMode.Impulse);
    //         // transform.forward = bullet.GetComponent<Rigidbody>().velocity;
    //     }
    //     yield return new WaitForSeconds(waitTime);
    //     canAttack = true;
    // }
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
            // transform.forward = finalDirection;

            rb.AddForce(rb.position + (rightMovement + upMovement));
            // rb.MovePosition(rb.position + (rightMovement + upMovement));
        }
    }
    public void Levitate()
    {
        StartCoroutine(LevitateCoroutine());
    }
    private IEnumerator LevitateCoroutine()
    {
        while (true)
        {
            if (transform.position.y < 1)
            {
                rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * 9, 0), ForceMode.Acceleration);
            }
            yield return new WaitForEndOfFrame();
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
