using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float movementSmoothTime = 0.1f;
    public float accelerationTime = 0.08f;

    [Header("Rotation Settings")]
    public float rotationSmoothTime = 0.08f;
    private float rotationVelocity;

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float jumpSmoothing = 0.15f;

    [Header("Dash Settings")]
    public float dashDistance = 5f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;
    public AnimationCurve dashCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Animation")]
    public Animator animator;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 currentDirection;
    private Vector3 directionVelocity;
    private Vector3 lastFacingDirection = Vector3.forward;

    private float verticalVelocity;
    private float   currentSpeed;
    private float speedVelocity;

    private bool isDashing;
    private float dashTimer;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // INPUT CALLBACKS
    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void OnJump(InputValue value)
    {
        if (controller.isGrounded && value.isPressed)
            StartCoroutine(SmoothJump());
    }

    public void OnDash(InputValue value)
    {
        if (!isDashing && dashTimer <= 0f && moveInput != Vector2.zero && value.isPressed)
            StartCoroutine(SmoothDash());
    }

    void Update()
    {
        HandleMovementAndRotation();

        if (dashTimer > 0f)
            dashTimer -= Time.deltaTime;

        animator.SetFloat("Speed", currentSpeed);
    }

    private void HandleMovementAndRotation()
    {
        Vector3 targetDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        currentDirection = Vector3.SmoothDamp(currentDirection, targetDirection, ref directionVelocity, movementSmoothTime);

        if (currentDirection.sqrMagnitude > 0.001f)
            lastFacingDirection = currentDirection;

        float targetAngle = Mathf.Atan2(lastFacingDirection.x, lastFacingDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        float targetSpeed = isDashing ? dashDistance / dashDuration : moveSpeed;
        float desiredSpeed = targetDirection.magnitude * targetSpeed;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, desiredSpeed, ref speedVelocity, accelerationTime);

        if (!isDashing)
        {
            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = -2f;
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = currentDirection * currentSpeed;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    IEnumerator SmoothJump()
    {
        float elapsed = 0f;
        float initialVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
        float startVel = verticalVelocity;

        while (elapsed < jumpSmoothing)
        {
            verticalVelocity = Mathf.Lerp(startVel, initialVel, elapsed / jumpSmoothing);
            elapsed += Time.deltaTime;
            yield return null;
        }
        verticalVelocity = initialVel;
    }

    IEnumerator SmoothDash()
    {
        isDashing = true;
        dashTimer = dashCooldown;

        float elapsed = 0f;
        Vector3 dashDir = lastFacingDirection.normalized;

        while (elapsed < dashDuration)
        {
            float t = elapsed / dashDuration;
            float dashSpeed = (dashDistance / dashDuration) * dashCurve.Evaluate(t);
            controller.Move(dashDir * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}
