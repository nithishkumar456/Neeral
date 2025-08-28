using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f;
    private CharacterController controller;

    private Vector2 moveInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void Update()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }
 
}
