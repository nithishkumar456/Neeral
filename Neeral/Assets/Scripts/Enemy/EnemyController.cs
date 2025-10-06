using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [Header("Ranges")]
    public float lookRadius = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    private Health health;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        // Stop just before attack range
        agent.stoppingDistance = attackRange * 0.9f;

        target = PlayerManager.instance.player.transform;

        // Subscribe to health changes
        health.OnHealthChanged += HandleHealthChanged;

        StartCoroutine(UpdatePath());
    }

    private void HandleHealthChanged(float current, float max)
    {
        if (current <= 0f && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("Attack", false);
        animator.SetFloat("Speed", 0f);
        animator.SetTrigger("Die"); 

        StopAllCoroutines();
        this.enabled = false;
    }

    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (isDead) yield break; // stop logic if dead

            if (target != null)
            {
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius && distance > agent.stoppingDistance)
                {
                    // Chase
                    agent.isStopped = false;
                    agent.SetDestination(target.position);

                    animator.SetFloat("Speed", agent.velocity.magnitude * (5f / agent.speed));
                    animator.SetBool("Attack", false);
                }
                else if (distance <= agent.stoppingDistance)
                {
                    // Attack   
                    agent.isStopped = true;
                    FaceTarget();
                    animator.SetFloat("Speed", 0f);
                    animator.SetBool("Attack", true);
                }
                else
                {
                    // Idle
                    agent.isStopped = true;
                    animator.SetFloat("Speed", 0f);
                    animator.SetBool("Attack", false);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
