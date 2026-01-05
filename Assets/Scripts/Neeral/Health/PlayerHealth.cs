using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int playerMaxHealth = 100;
    public int PlayerMaxHealth => playerMaxHealth;
    public int CurrentHealth { get; private set; }

    [Header("Invincibility")]
    [SerializeField] private float invincibleDuration = 0.5f;
    private bool isInvincible = false;
    private float invincibleTimer = 0f;

    private bool isDead = false;
    public bool IsDead => isDead;

    private PlayerAnimator playerAnimator;
    private PlayerMovement movement;
    private PlayerCombat combat;
    private PlayerWeaponManager weaponManager;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        weaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Start()
    {
        CurrentHealth = playerMaxHealth;
    }

    private void Update()
    {
        HandleInvincibility();
    }

    private void HandleInvincibility()
    {
        if (!isInvincible) return;

        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer <= 0f)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || isInvincible) return;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, playerMaxHealth);

        // Play hit animation
/*         playerAnimator.PlayHit(); */

        // Start invincibility frames
        isInvincible = true;
        invincibleTimer = invincibleDuration;

        // Check for death
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, playerMaxHealth);
    }

    public void Die()
    {
        isDead = true;

        // Play death animation
        /* playerAnimator.PlayDeath(); */

        // Disable player control
        movement.enabled = false;
        combat.enabled = false;
        weaponManager.enabled = false;

        // Later: trigger game over, respawn, etc.
    }
}
