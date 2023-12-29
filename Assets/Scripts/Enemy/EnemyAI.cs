using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float chaseDistance = 5.0f;
    public float attackDistance = 1.5f;
    public float attackDamage = 10.0f;
    public float attackCooldown = 1.0f;
    public float patrolDistance = 10.0f; // Distance each enemy can move from its start position
    public float initialChaseDelay = 10f; // Delay before starting to chase

    private Transform playerTransform;
    private Vector3 randomDestination;
    private Vector3 startPosition;
    private float changeDestinationTime = 5.0f;
    private float changeDestinationTimer;
    private float attackTimer;
    private float chaseTimer;

    private bool isInitialized = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position; // Store the starting position
        SetRandomDestination();
        chaseTimer = initialChaseDelay; // Initialize chase timer
    }

void Update()
{
    // If the AI is not initialized and the player has spawned, initialize the AI
    if (!isInitialized && GameObject.FindGameObjectWithTag("Player") != null)
    {
        Initialize();
    }

    // Only execute AI logic if the AI is initialized
    if (isInitialized)
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (chaseTimer > 0)
            {
                chaseTimer -= Time.deltaTime;
            }
            else
            {
                if (distanceToPlayer <= attackDistance)
                {
                    if (attackTimer <= 0)
                    {
                        AttackPlayer();
                        attackTimer = attackCooldown;
                    }
                    else
                    {
                        attackTimer -= Time.deltaTime;
                    }
                }
                else if (distanceToPlayer <= chaseDistance)
                {
                    ChasePlayer();
                }
                else
                {
                    MoveRandomly();
                }
            }
        }
    }
}

    void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position; // Store the starting position
        SetRandomDestination();
        chaseTimer = initialChaseDelay; // Initialize chase timer
        isInitialized = true;
    }

    void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.LookAt(playerTransform);
        transform.position += direction * moveSpeed * Time.deltaTime; // Directly setting the position
    }

    void MoveRandomly()
    {
        if (Vector3.Distance(transform.position, startPosition) > patrolDistance)
        {
            // If the enemy is too far from its start position, move back to the start position
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // If the enemy is within its patrol distance, move to a random destination
            transform.position = Vector3.MoveTowards(transform.position, randomDestination, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, randomDestination) < 0.5f || changeDestinationTimer > changeDestinationTime)
            {
                SetRandomDestination();
                changeDestinationTimer = 0;
            }

            changeDestinationTimer += Time.deltaTime;
        }
    }

    void SetRandomDestination()
    {
        // Generate a random angle
        float randomAngle = Random.Range(0, 360);

        // Calculate a random direction
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));

        // Multiply the random direction by the patrol distance to get a random destination within the patrol distance
        randomDestination = startPosition + randomDirection * patrolDistance;
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
