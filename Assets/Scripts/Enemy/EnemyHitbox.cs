using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [Header("Hit Settings")]
    public Transform enemyRoot;
    public float hitRadius = 1.0f;
    public int damage = 10;

    public void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(enemyRoot.position, hitRadius);

        foreach (Collider hit in hits)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyRoot == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyRoot.position, hitRadius);
    }
}
