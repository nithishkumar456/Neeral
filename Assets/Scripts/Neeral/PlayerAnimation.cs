using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        animator.SetInteger("AttackIndex", 0);
    }

    private void Update()
    {
        float speed = movement.GetCurrentSpeed();
        animator.SetFloat("Speed", speed);
    }

    public void PlayAttack(int index)
    {
        animator.SetInteger("AttackIndex", index);
        animator.SetTrigger("Attack");
    }

    public void ResetAttackIndex()
    {
        animator.SetInteger("AttackIndex", 0);
    }
}
