using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float chaseDistance = 5.0f;
    public float attackDistance = 1.5f;

    private Transform playerTransform;
    private Vector3 randomDestination;
    private float changeDestinationTime = 5.0f;
    private float changeDestinationTimer;

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
            // Stop moving when in attack range
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            // Chase the player
            ChasePlayer();
        }
        else
        {
            // Move randomly
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
}
