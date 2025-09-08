using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using System.ComponentModel;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    private float attackRange = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange - 0.1f;

        target = PlayerManager.instance.player.transform;
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (target != null)
            {
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius)
                {
                    agent.SetDestination(target.position);
                }
            }
            yield return new WaitForSeconds(0.2f); // update 5 times per second
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
