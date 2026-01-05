using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Image healthFill;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (enemyHealth == null) return;

        transform.LookAt(transform.position + cam.transform.forward);

        float fill = (float)enemyHealth.CurrentHealth / enemyHealth.MaxHealth;
        healthFill.fillAmount = fill;

        gameObject.SetActive(!enemyHealth.IsDead);
    }
}
