using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    public float health = 100.0f;
    public TMP_Text healthText; // Use TMP_Text for TextMeshPro elements

    void Start()
    {
        UpdateHealthText(); // Initial update to display health at the start
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = string.Format("Health: {0:D3}%", Mathf.Clamp((int)health, 0, 100));
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player has died");
            // Here you can handle the player's death, e.g. trigger a death animation
        }
        UpdateHealthText(); // Update the health text every time damage is taken
    }
}
