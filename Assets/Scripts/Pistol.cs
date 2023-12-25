using UnityEngine;
using System.Collections;
using TMPro; // Include the TextMeshPro namespace

public class Pistol : MonoBehaviour
{
    public float damage = 5f;
    public float range = 50f;
    public float impactForce = 30f;
    // public float fireRate = 15f;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    // private float nextTimeToFire = 0f;
    public Animator animator;
    public TextMeshProUGUI PistolAmmoText; // Reference to the TextMeshPro UI element

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText(); // Initialize ammo display
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
        ShowAmmoText(); // Make sure ammo text is shown when the pistol is enabled
    }

    void OnDisable()
    {
        HideAmmoText(); // Hide ammo text when the pistol is disabled
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

IEnumerator Reload()
{
    isReloading = true;
    Debug.Log("Reloading...");
    animator.SetBool("Reloading", true);

    yield return new WaitForSeconds(reloadTime - 0.25f);
    animator.SetBool("Reloading", false);
    yield return new WaitForSeconds(0.25f);

    currentAmmo = maxAmmo;
    isReloading = false;
    UpdateAmmoText(); // Call UpdateAmmoText here to update the ammo display immediately after reloading
}


    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
        }
        UpdateAmmoText(); // Update ammo display after each shot
    }

    // This method will be called to update the ammo display
    void UpdateAmmoText()
    {
        if (PistolAmmoText != null)
        {
            PistolAmmoText.text = currentAmmo.ToString();
        }
    }

    // Call this method to hide the ammo text
    public void HideAmmoText()
    {
        if (PistolAmmoText != null)
        {
            PistolAmmoText.gameObject.SetActive(false);
        }
    }

    // Call this method to show the ammo text
    public void ShowAmmoText()
    {
        if (PistolAmmoText != null)
        {
            PistolAmmoText.gameObject.SetActive(true);
        }
    }
}
