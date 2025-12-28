using UnityEngine;
using System.Collections.Generic;

public class WeaponHitbox : MonoBehaviour
{
    private bool canDealDamage = false;
    private HashSet<EnemyAnimator> enemiesHit = new HashSet<EnemyAnimator>();

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

        EnemyAnimator enemyAnim = other.GetComponentInParent<EnemyAnimator>();
        if (enemyAnim == null) return;

        if (enemiesHit.Contains(enemyAnim)) return;

        enemiesHit.Add(enemyAnim);
        enemyAnim.PlayHit();
    }
}
