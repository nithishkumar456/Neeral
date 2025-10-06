using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour
{
    public float damage = 10f;
    private bool canDealDamage = false;

    private void OnTriggerEnter(Collider other)
    {
       if (!canDealDamage) return;

        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage from {gameObject.name}");
            }
        }
    }

    // Called from animation event at the moment the hit should start
    public void EnableDamage()
    {
        canDealDamage = true;
    }

    // Called from animation event when the hit ends
    public void DisableDamage()
    {
        canDealDamage = false;
    }
}
