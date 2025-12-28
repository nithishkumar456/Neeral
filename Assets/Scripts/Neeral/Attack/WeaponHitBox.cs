using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private bool canDealDamage = false;

    public void EnableHitbox()
    {
        canDealDamage = true;
    }

    public void DisableHitbox()
    {
        canDealDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;

        EnemyAnimator enemyAnim = other.GetComponent<EnemyAnimator>();
        if (enemyAnim != null)
        {
            enemyAnim.PlayHit();
        }

        // Later:
        // other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
    }
}
