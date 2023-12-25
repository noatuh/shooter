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

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position; // Store the starting position
        SetRandomDestination();
        chaseTimer = initialChaseDelay; // Initialize chase timer
    }

    void Update()
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

    void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.LookAt(playerTransform);
        transform.position += direction * moveSpeed * Time.deltaTime; // Directly setting the position
    }

    void MoveRandomly()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomDestination, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, randomDestination) < 0.5f || changeDestinationTimer > changeDestinationTime)
        {
            SetRandomDestination();
            changeDestinationTimer = 0;
        }

        changeDestinationTimer += Time.deltaTime;
    }

    void SetRandomDestination()
    {
        // Generate a random destination within a "patrolDistance" radius from the starting position
        float randomX = Random.Range(startPosition.x - patrolDistance, startPosition.x + patrolDistance);
        float randomZ = Random.Range(startPosition.z - patrolDistance, startPosition.z + patrolDistance);
        randomDestination = new Vector3(randomX, startPosition.y, randomZ);
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
