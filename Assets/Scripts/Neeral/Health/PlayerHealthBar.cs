using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image healthFill;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (playerHealth == null) return;

        float fill = (float)playerHealth.CurrentHealth / playerHealth.PlayerMaxHealth;
        healthFill.fillAmount = fill;

        gameObject.SetActive(!playerHealth.IsDead);
    }
}
