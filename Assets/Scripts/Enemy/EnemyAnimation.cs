using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        float speed = agent.velocity.magnitude;

        animator.SetFloat("Speed", speed);
    }
    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }
    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }
    public void PlayDeath()
    {
        animator.SetTrigger("Die");
    }
    
}
