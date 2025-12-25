using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private Vector2 moveInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnSprint(InputValue value)
    {
        if (value.isPressed)
            animator.SetTrigger("Dash");
    }

    private void Update()
    {
        if (!movement.IsDashing())
        {
            float speed = moveInput.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
}
