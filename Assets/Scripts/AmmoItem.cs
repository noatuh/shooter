using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public int ammoAmount = 90; // Amount of ammo to replenish

    public void ReplenishAmmo(GameObject player)
    {
        Rifle rifle = player.GetComponent<Rifle>();
        if (rifle != null)
        {
            rifle.AddAmmo(ammoAmount);
        }
        // Optionally, destroy the ammo item after use
        // Destroy(gameObject);
    }
}
