using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float chaseDistance = 5.0f;
    public float attackDistance = 1.5f;
    public float attackDamage = 10.0f; // Public so you can modify it later in the Inspector
    public float attackCooldown = 5.0f; // Time in seconds between attacks

    private Transform playerTransform;
    private Vector3 randomDestination;
    private float changeDestinationTime = 5.0f;
    private float changeDestinationTimer;
    private float attackTimer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackDistance)
        {
            if (attackTimer <= 0)
            {
                AttackPlayer();
                attackTimer = attackCooldown; // Reset the attack timer
            }
            else
            {
                attackTimer -= Time.deltaTime; // Decrease timer
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

    void ChasePlayer()
    {
        transform.LookAt(playerTransform);
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    void MoveRandomly()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomDestination, moveSpeed * Time.deltaTime);

        // Calculate the new direction to look at
        Vector3 direction = (randomDestination - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f); // Adjust the rotation speed if necessary
        }

        if (Vector3.Distance(transform.position, randomDestination) < 0.5f || changeDestinationTimer > changeDestinationTime)
        {
            SetRandomDestination();
            changeDestinationTimer = 0;
        }

        changeDestinationTimer += Time.deltaTime;
    }

    void SetRandomDestination()
    {
        float randomX = Random.Range(-10.0f, 10.0f);
        float randomZ = Random.Range(-10.0f, 10.0f);
        randomDestination = new Vector3(randomX, transform.position.y, randomZ);
    }

    void AttackPlayer()
    {
        // Assuming the player has a script named PlayerHealth attached to it
        PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
