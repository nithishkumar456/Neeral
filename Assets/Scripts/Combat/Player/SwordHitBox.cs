using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    private Collider hitbox;

    private void Awake()
    {
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false; 
    }
    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponentInParent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Animator enemyAnimator = other.GetComponentInParent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Hit");
            }
        }
    }
}
