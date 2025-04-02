using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy_Goblin : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float detectionRadius = 0.01f;
    [SerializeField]
    float atackRadius = 0.01f;
    [SerializeField]
    bool patrol;
    [SerializeField]
    Vector3[] patrolPoints;
    [SerializeField]
    float waitTime = 2f;

    bool atacking;

    Vector3 initialPosition;
    NavMeshAgent agent;
    bool isPlayerInRange;

    int currentPatrolIndex;
    float patrolWaitTimer;

    Animator anim;

    void Start()
    {
        player = FindAnyObjectByType<Player_Actions>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        initialPosition = transform.position;

        currentPatrolIndex = 0;
        patrolWaitTimer = waitTime;

        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (this.gameObject.GetComponent<Gobling_Interactions>().death)
        {
            agent.isStopped = true;

        }
        else
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= detectionRadius)
            {
                if (!isPlayerInRange)
                {
                    isPlayerInRange = true;
                    agent.isStopped = false;
                }
                agent.SetDestination(player.position);
            }
            else
            {
                if (isPlayerInRange)
                {
                    isPlayerInRange = false;
                }

                if (patrol)
                {
                    Patrol();
                }
                else
                {
                    agent.SetDestination(initialPosition);
                }
            }

            if (distanceToPlayer <= atackRadius && !atacking)
            {
                atacking = true;
                agent.isStopped = true;
                this.gameObject.GetComponent<Gobling_Interactions>().Atack();
                StartCoroutine(EndAtack());

            }
        }

        if (agent.velocity.y > 0)
        {
            anim.SetFloat("Y", 1);
        }
        else if (agent.velocity.y < 0)
        {
            anim.SetFloat("Y", -1);

        }
        else if (agent.velocity.y == 0)
        {
            anim.SetFloat("Y", 0);
        }

        if (agent.velocity.x > 0)
        {
            anim.SetFloat("X", 1);
        }
        else if (agent.velocity.x < 0)
        {
            anim.SetFloat("X", -1);
        }
        else if (agent.velocity.x == 0)
        {
            anim.SetFloat("X", 0);
        }


    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            patrol = false;
            return;
        }

        if (agent.remainingDistance < 0.001f && !agent.pathPending)
        {
            patrolWaitTimer -= Time.deltaTime;
            if (patrolWaitTimer <= 0f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex]);
                patrolWaitTimer = waitTime;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atackRadius);
    }

    private IEnumerator EndAtack()
    {
        yield return new WaitForSeconds(3);
        atacking = false;
        agent.isStopped = false;

        if (isPlayerInRange)
        {
            agent.SetDestination(player.position);
        }
        else if (patrol)
        {
            Patrol();
        }
        else
        {
            agent.SetDestination(initialPosition);

        }
    }

}
