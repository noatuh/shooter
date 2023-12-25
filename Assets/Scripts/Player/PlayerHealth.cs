using UnityEngine;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.SceneManagement; // Namespace for Scene Management

public class PlayerHealth : MonoBehaviour
{
    public float health = 100.0f;
    public TMP_Text healthText;

    void Start()
    {
        UpdateHealthText(); // Initial update to display health at the start
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = string.Format("Health: {0:D3}%", Mathf.Clamp((int)health, 0, 100));
        }
    }

    void Die()
    {
        // Add any death animation or sound effects here if needed

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Example function to take damage
    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthText();

        if (health <= 0)
        {
            Die();
        }
    }

    // Function to replenish health
    public void ReplenishHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100); // Ensure health doesn't exceed 100
        UpdateHealthText();
    }
}
