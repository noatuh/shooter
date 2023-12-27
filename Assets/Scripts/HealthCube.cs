using UnityEngine;
using TMPro; // Use TextMeshPro namespace

public class HealthCube : MonoBehaviour
{
    public bool destroyAfterUse;
    public float healAmount;
    public TextMeshProUGUI healthPromptText; // Change to TextMeshProUGUI for the prompt

    private GameObject player;
    private bool playerInRange;
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // Get the PlayerHealth component from the player object
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (healthPromptText != null)
        {
            healthPromptText.enabled = false; // Ensure the prompt is not visible initially
        }
    }

    void Update()
    {
        if (player != null && healthPromptText != null && playerHealth != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            playerInRange = distance < 3.0f;

            // Enable or disable the health prompt based on player's proximity and line of sight
            if (playerInRange && IsPlayerLookingAtCube())
            {
                healthPromptText.enabled = true;
            }
            else
            {
                healthPromptText.enabled = false;
            }

            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                HealPlayer();
            }
        }
    }

    private bool IsPlayerLookingAtCube()
    {
        RaycastHit hit;
        Transform cameraTransform = Camera.main.transform; // Get the main camera transform

        // Perform a raycast from the camera forward
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
        {
            // Check if the hit object is the health cube
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }

    private void HealPlayer()
    {
        // Only heal the player if their health is not already at maximum
        if (playerHealth.health < playerHealth.maxHealth)
        {
            // Call ReplenishHealth on the player's PlayerHealth script
            playerHealth.ReplenishHealth(healAmount);

            Debug.Log("Player has been healed by " + healAmount);

            if (destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
