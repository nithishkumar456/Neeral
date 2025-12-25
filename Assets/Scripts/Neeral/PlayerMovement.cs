using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public Transform cameraTransform;

    public bool CanMove = true;

    private CharacterController controller;
    private Vector2 moveInput;
    private bool isDashing;
    private float dashTimer;
    private float rotationVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        if (value.isPressed && !isDashing && CanMove)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = cameraTransform.TransformDirection(move);
        move.y = 0;

        if (CanMove)
            RotatePlayer(move);

        if (CanMove)
        {
            if (isDashing)
            {
                controller.Move(move * dashSpeed * Time.deltaTime);
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                    isDashing = false;
            }
            else
            {
                controller.Move(move * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void RotatePlayer(Vector3 move)
    {
        if (move.sqrMagnitude < 0.01f)
            return;

        float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public float GetCurrentSpeed() => moveInput.magnitude;
    public bool IsDashing() => isDashing;
}
