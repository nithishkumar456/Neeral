using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }

    private bool isDead = false;
    public bool IsDead => isDead;

    private EnemyAnimator enemyAnimator;
    private EnemyAI enemyAI;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        enemyAnimator.PlayHit();
        if (isDead) return;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        enemyAnimator.PlayDeath();
        enemyAI.enabled = false;

        foreach (Collider col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        Destroy(gameObject, 0.2f);
    }
}
