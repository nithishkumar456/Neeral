using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    private PlayerMovement movement;

    private int attackIndex = 0;
    private bool canCombo = false;
    private bool isAttacking = false;

    private float comboResetTimer = 0f;
    private float comboResetTime = 1f;

    [Header("Hit Detection")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackRadius = 0.7f;
    [SerializeField] private LayerMask enemyLayer;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            movement.CanMove = false;

            attackIndex = 1;
            playerAnimator.PlayAttack(attackIndex);
            isAttacking = true;
            return;
        }

        if (canCombo && attackIndex < 3)
        {
            attackIndex++;
            playerAnimator.PlayAttack(attackIndex);
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
        playerAnimator.ResetAttackIndex();
        isAttacking = false;
        canCombo = false;
        comboResetTimer = 0f;
        movement.CanMove = true;
    }

    // Called by animation event
    public void AttackHit()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position + transform.forward * attackRange,
            attackRadius,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            EnemyAnimator enemyAnim = hit.GetComponent<EnemyAnimator>();
            if (enemyAnim != null)
                enemyAnim.PlayHit();

            // Later:
            // hit.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRadius);
    }
}
