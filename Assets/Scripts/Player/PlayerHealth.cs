using UnityEngine;
using TMPro; // Make sure to include TextMeshPro if you're using TMP_Text

public class PlayerHealth : MonoBehaviour
{
    public float health = 100.0f;
    public TMP_Text healthText; // Make sure this is linked in the Unity inspector

    private void Start()
    {
        UpdateHealthText();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + Mathf.Clamp(health, 0, 100) + "%";
    }

    public void Die()
    {
        // Add your death logic here (animations, game over screen, etc.)
        Debug.Log("Player has died.");
        // Reload the scene or reset the game
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthText();
        if (health <= 0)
        {
            Die();
        }
    }

    public void ReplenishHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100); // Ensure health does not exceed 100
        UpdateHealthText();
        Debug.Log("Health replenished by " + amount + ". Current health: " + health);
    }
}
