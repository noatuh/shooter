using UnityEngine;
using TMPro; // Use the TextMeshPro namespace

public class AmmoItem : MonoBehaviour

{

    public bool destroyAfterUse = true; // Destroy the ammo item after use
    public int ammoAmount = 90; // Amount of ammo to replenish
    public TextMeshProUGUI ammoPromptText; // Add a TextMeshProUGUI variable for the ammo prompt

    private GameObject player;
    private bool playerInRange;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ammoPromptText.enabled = false; // Ensure the prompt is not visible initially
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            playerInRange = distance < 3.0f;

            // Enable or disable the ammo prompt based on player's proximity and line of sight
            if (playerInRange && IsPlayerLookingAtAmmo())
            {
                ammoPromptText.enabled = true;
            }
            else
            {
                ammoPromptText.enabled = false;
            }
        }
    }

    private bool IsPlayerLookingAtAmmo()
    {
        RaycastHit hit;
        Transform cameraTransform = Camera.main.transform; // Get the main camera transform

        // Perform a raycast from the camera forward
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
        {
            // Check if the hit object is the ammo item
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }

    public void ReplenishAmmo(GameObject player)
    {
        Rifle rifle = player.GetComponent<Rifle>();
        if (rifle != null && rifle.totalAmmo < 200) // Check if the player has less than 200 ammo
        {
            rifle.AddAmmo(ammoAmount);

            // Disable the ammo prompt before destroying the ammo item
            ammoPromptText.enabled = false;

            // Destroy the ammo item after use if destroyAfterUse is true
            if (destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
    }

}
