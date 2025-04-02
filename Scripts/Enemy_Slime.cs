using UnityEngine;
using UnityEngine.AI;

public class Enemy_Slime : MonoBehaviour
{
    public Transform player;
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

    bool facingRight = true;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = FindAnyObjectByType<Player_Actions>().transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        initialPosition = transform.position;

        currentPatrolIndex = 0;
        patrolWaitTimer = waitTime;

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (this.gameObject.GetComponent<Enemy_Interactions>().death || atacking)
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
                this.gameObject.GetComponent<Enemy_Interactions>().Atack();

            }

            FlipSprite(agent.velocity.x);
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

    void FlipSprite(float moveDirection)
    {
        if (moveDirection > 0.1 && !facingRight)
        {
            facingRight = true;
            spriteRenderer.flipX = true;
        }
        else if (moveDirection < 0.1 && facingRight)
        {
            facingRight = false;
            spriteRenderer.flipX = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atackRadius);
    }

    public void EndAtack()
    {
        Debug.Log("Hola");
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
