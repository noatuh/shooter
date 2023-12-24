using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100.0f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player has died");
            // Additional logic for player death can be added here
        }
    }
}
