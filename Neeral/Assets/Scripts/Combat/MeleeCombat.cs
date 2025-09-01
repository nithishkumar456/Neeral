using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;

    private int comboStep = 0;
    private bool isAttacking = false;
    private float comboResetTime = 1f;
    private float lastAttackTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Attack"].performed += HandleAttack;
    }

    private void OnEnable()
    {
        playerInput.actions["Attack"].Enable();
    }

    private void OnDisable()
    {
        playerInput.actions["Attack"].Disable();
    }

    private void HandleAttack(InputAction.CallbackContext context)
    {
        // Reset combo if idle too long
        if (isAttacking && Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
        }

        comboStep++;

        if (comboStep == 1)
            PlayAttack("Attack1");
        else if (comboStep == 3)
            PlayAttack("Attack2");
        else if (comboStep == 4)
            PlayAttack("Attack3");

        lastAttackTime = Time.time;
    }

    private void PlayAttack(string animationName)
    {
        animator.Play(animationName, 0, 0f);
        isAttacking = true;
    }
    
    public void EndAttack()
    {
        isAttacking = false;
        animator.Play("Idle");
    }
}
