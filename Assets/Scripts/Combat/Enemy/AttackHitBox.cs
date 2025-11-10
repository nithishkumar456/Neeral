using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 50f;
    private bool canDealDamage = false;
    private bool hasDealtDamage = false;

    public void EnableDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }

    public void DisableDamage()
    {
        canDealDamage = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canDealDamage || hasDealtDamage)
        {
            hasDealtDamage = true;
            return;
        }

        if (other.CompareTag("Player"))
        {
            var health = other.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                hasDealtDamage = true;
            }
        }
    }
}
