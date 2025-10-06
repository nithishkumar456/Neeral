using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Health))]
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private float smoothDuration = 0.15f;

    private Health health;
    private Coroutine smoothRoutine;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnHealthChanged += HandleHealthChanged;
        health.OnMaxHealthChanged += HandleMaxChanged;

        // Initial sync
        HandleMaxChanged(health.MaxHealth);
        HandleHealthChanged(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= HandleHealthChanged;
        health.OnMaxHealthChanged -= HandleMaxChanged;
    }

    private void HandleMaxChanged(float newMax)
    {
        if (slider == null) return;
        slider.maxValue = newMax;
        slider.value = Mathf.Clamp(slider.value, 0f, slider.maxValue);
    }

    private void HandleHealthChanged(float current, float max)
    {
        if (slider == null) return;

        if (!Mathf.Approximately(slider.maxValue, max))
            slider.maxValue = max;

        if (smoothRoutine != null)
            StopCoroutine(smoothRoutine);

        if (isPlayer)
        {
            // Player: instant update
            slider.value = current;
        }
        else
        {
            // Enemy: smooth drop
            smoothRoutine = StartCoroutine(SmoothTo(current));
        }
    }

    private IEnumerator SmoothTo(float target)
    {
        float start = slider.value;
        float elapsed = 0f;

        if (Mathf.Approximately(start, target)) yield break;

        while (elapsed < smoothDuration)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(start, target, Mathf.Clamp01(elapsed / smoothDuration));
            yield return null;
        }

        slider.value = target;
        smoothRoutine = null;
    }
}
