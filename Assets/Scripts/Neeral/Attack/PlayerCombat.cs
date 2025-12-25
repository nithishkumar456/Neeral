using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;

    private int attackIndex = 0;
    private bool canCombo = false;
    private bool isAttacking = false;

    private float comboResetTimer = 0f;
    private float comboResetTime = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            movement.CanMove = false;

            attackIndex = 1;
            animator.SetInteger("AttackIndex", attackIndex);
            animator.SetTrigger("Attack");
            isAttacking = true;
            return;
        }

        if (canCombo && attackIndex < 3)
        {
            attackIndex++;
            animator.SetInteger("AttackIndex", attackIndex);
            canCombo = false;
        }
    }

    private void Update()
    {
        if (isAttacking)
        {
            comboResetTimer += Time.deltaTime;
            if (comboResetTimer >= comboResetTime)
                ResetCombo();
        }
    }

    public void EnableCombo()
    {
        canCombo = true;
        comboResetTimer = 0f;
    }

    public void EndAttack()
    {
        ResetCombo();
    }

    private void ResetCombo()
    {
        attackIndex = 0;
        animator.SetInteger("AttackIndex", 0);
        isAttacking = false;
        canCombo = false;
        comboResetTimer = 0f;
        movement.CanMove = true;
    }
}
