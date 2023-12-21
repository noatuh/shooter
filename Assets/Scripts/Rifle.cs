using UnityEngine;

public class Rifle : MonoBehaviour
{
    void Update()
    {
        // Check if the Fire1 action is triggered
        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        // Implement your firing logic here
        Debug.Log("Weapon Fired!");
    }
}
