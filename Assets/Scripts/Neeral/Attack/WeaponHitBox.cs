using UnityEngine;
using System.Collections.Generic;

public class WeaponHitbox : MonoBehaviour
{
    private bool canDealDamage = false;

    private HashSet<EnemyHealth> enemiesHit = new HashSet<EnemyHealth>();

    public void EnableHitbox()
    {
        canDealDamage = true;
        enemiesHit.Clear();
    }

    public void DisableHitbox()
    {
        canDealDamage = false;
        enemiesHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;

        EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
        if (enemyHealth == null) return;

        if (enemiesHit.Contains(enemyHealth)) return;

        enemiesHit.Add(enemyHealth);

        enemyHealth.TakeDamage(10);
    }
}
