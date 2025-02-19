using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float walkSpeed;

    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float groundDrag;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("KeyBinds")]
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        Walking,
        Dashing,
        Crouching,
        air
    }

    public bool isDashing;

    public PlayerCam playerCam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // get the original scale of the player
        startYScale = transform.localScale.y;
        // get the original speed of the player
        walkSpeed = moveSpeed;

        // Ensure playerCam is assigned
        if (playerCam == null)
        {
            playerCam = GetComponentInChildren<PlayerCam>();
        }
    }

    void Update()
    {
        // ground check raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        if (state == MovementState.Walking || state == MovementState.Crouching)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        MyInput();
        SpeedControl();
        StateHandler();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomemtum;

    // handle player state
    private void StateHandler()
    {
        if (isDashing)
        {
            state = MovementState.Dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangefactor = dashSpeedChangeFactor;

            // Change fov
            playerCam.DoFov(75f);
        }
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.Crouching;
            desiredMoveSpeed = crouchSpeed;

            // Reset fov
            playerCam.DoFov(60f);
        }
        else if (isGrounded)
        {
            state = MovementState.Walking;
            desiredMoveSpeed = walkSpeed;

            // Reset fov
            playerCam.DoFov(60f);
        }
        else
        {
            state = MovementState.air;
        }

        bool desiredMoveSpeedChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.Dashing) keepMomemtum = true;

        if (desiredMoveSpeedChanged)
        {
            if (keepMomemtum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private float speedChangefactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangefactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangefactor = 1f;
        keepMomemtum = false;
    }

    // player input
    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            // set player scale to crouch scale
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            // add force to push player down
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            // set player scale back to normal
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            moveSpeed = walkSpeed;
        }
    }

    // move the player
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    // draw gizmo ground check ray
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f));
    }

    // limit player speed
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
