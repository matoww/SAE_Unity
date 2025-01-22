using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class foncedessus : MonoBehaviour
{
    public float patrolSpeed = 2f;    
    public float chaseSpeed = 4f;     
    public float detectionRadius = 5f;  
    public float stopDistance = 0.5f;   
    public float patrolWaitTime = 2f;  

    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    private bool isPatrolling = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            if (!isPatrolling)
            {
                StartCoroutine(Patrol());
            }
        }
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);


        if (Vector3.Distance(transform.position, player.position) <= stopDistance)
        {
            agent.SetDestination(player.position);  
        }
    }

    IEnumerator Patrol()
    {
        isPatrolling = true;

        while (!isChasing)
        {
            
            Vector3 randomPos = RandomNavmeshLocation(20f);

            agent.SetDestination(randomPos);
            yield return new WaitForSeconds(patrolWaitTime);  
        }

        isPatrolling = false;
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        
        return transform.position;  
    }
}
