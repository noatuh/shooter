using UnityEngine;

public class HealthCube : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Ensure your player has the tag "Player"
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 3.0f) // 3.0f is the interaction distance
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.ReplenishHealth(10); // Replenish 10 health
                }
            }
        }
    }
}
