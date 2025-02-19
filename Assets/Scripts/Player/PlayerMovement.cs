using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float originalMoveSpeed;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // get the original scale of the player
        startYScale = transform.localScale.y;
        // get the original speed of the player
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // ground check raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        MyInput();
        SpeedControl();
    }

    void FixedUpdate()
    {
        MovePlayer();
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
            moveSpeed = crouchSpeed;
        }

        // end crouch
        if (Input.GetKeyUp(crouchKey))
        {
            // set player scale back to normal
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            moveSpeed = originalMoveSpeed;
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
