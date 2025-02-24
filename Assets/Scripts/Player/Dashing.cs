using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    public Transform orientation;
    public Transform playerCam;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;

    [Header("CameraEffects")]
    public PlayerCam pCam;
    public float dashFov;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVal = true;

    [Header("Cooldown")]
    public float dashCooldown;
    private float dashCooldownTimer;

    [SerializeField] private Image dashCooldownImage;

    //[Header("KeyBinds")]
    //public KeyCode dashKey = KeyCode.Space;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        dashCooldownImage.fillAmount = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
            Dash();

        DashCooldownUI();
    }

    private void DashCooldownUI()
    {
        if (dashCooldownTimer > 0)
        {
            // dash cooldown timer
            dashCooldownTimer -= Time.deltaTime;
            // dash cooldown image fill amount
            dashCooldownImage.fillAmount = 0 + (dashCooldownTimer / dashCooldown);
        }
        else
            dashCooldownImage.fillAmount = 0;
    }

    private void Dash()
    {
        if (dashCooldownTimer > 0) return;
        else dashCooldownTimer = dashCooldown;

        playerMovement.isDashing = true;

        // get the forward direction
        Transform forwardT;

        if (useCameraForward)
            forwardT = playerCam; // where the player looking
        else
            forwardT = orientation; // where the player facing

        // call the GetDirection method
        Vector3 direction = GetDirection(forwardT);

        // apply dash force
        Vector3 forceToApply = direction * dashForce;

        // disable gravity if needed
        if (disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    // delay the dash if needed
    private Vector3 delayedForceToApply;
    private void DelayDashForce()
    {
        if (resetVal)
            rb.velocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        playerMovement.isDashing = false;

        // reset gravity if needed
        if (disableGravity)
            rb.useGravity = true;
    }

    // direction math for the player
    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}
