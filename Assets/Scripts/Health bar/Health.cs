using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float MaxHealth => maxHealth;
    public float CurrentHealth { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action<float> OnMaxHealthChanged;

    private bool hasInitialised = false;

    private void Awake()
    {
        if (!hasInitialised)
        {
            CurrentHealth = maxHealth;
            hasInitialised = true;
        }

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0) return;
        float old = CurrentHealth;
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0f, maxHealth);
        if (!Mathf.Approximately(old, CurrentHealth))
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        if (CurrentHealth <= 0) Die();
    }

    public void Heal(float amount)
    {
        if (amount <= 0) return;
        float old = CurrentHealth;
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, maxHealth);
        if (!Mathf.Approximately(old, CurrentHealth))
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    // 🔹 Call this ONLY from your power-up script
    public void RestoreFullHealth()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void IncreaseMaxHealth(float amount, bool fillToMax = false)
    {
        if (Mathf.Approximately(amount, 0f)) return;
        maxHealth = Mathf.Max(0.01f, maxHealth + amount);
        if (fillToMax) CurrentHealth = maxHealth;
        OnMaxHealthChanged?.Invoke(maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log($"[{name}] died!");
    }
}
