using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;
    private float timeSinceAttack;
    private int attackPressCount = 0;

    [Header("Settings")]
    public float minTimeBetweenAttacks = 0.3f; // faster responsiveness
    public float comboResetThreshold = 1.0f;
    public bool isEquipped = true;

    private readonly string[] attackSequence = { "Attack1", "Attack1", "Attack2", "Attack3" };

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            playerInput.actions["Attack"].performed += HandleAttack;
        }
    }

    private void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.actions["Attack"].performed -= HandleAttack;
        }
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
    }

    private void HandleAttack(InputAction.CallbackContext context)
    {
        // Must be grounded, equipped, and past min time between attacks
        if (!animator.GetBool("Grounded") || timeSinceAttack < minTimeBetweenAttacks)
            return;

        if (!isEquipped)
            return;

        // Reset combo if too much time passed
        if (timeSinceAttack > comboResetThreshold)
        {
            attackPressCount = 0;
        }

        // Pick the correct attack from the sequence
        string attackAnim = attackSequence[attackPressCount];

        // Trigger attack quickly
        animator.CrossFade(attackAnim, 0.05f);

        // Move to next attack in sequence
        attackPressCount++;

        // If we've reached the end of the sequence, reset
        if (attackPressCount >= attackSequence.Length)
        {
            attackPressCount = 0;
        }
        timeSinceAttack = 0;
    }
}
