using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro namespace

public class Rifle : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public int maxAmmo = 30;
    private int currentAmmo;
    public int totalAmmo = 90;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public TextMeshProUGUI ammoDisplay; // Reference to the TextMeshProUGUI component
    public Animator animator;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
        SetAmmoDisplayVisibility(true); // Show ammo counter when rifle is enabled
    }

    void OnDisable()
    {
        SetAmmoDisplayVisibility(false); // Hide ammo counter when rifle is disabled
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && totalAmmo > 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && totalAmmo > 0))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                AmmoItem ammoItem = hit.transform.GetComponent<AmmoItem>();
                if (ammoItem != null)
                {
                    ammoItem.ReplenishAmmo(gameObject);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        int ammoNeeded = maxAmmo - currentAmmo;
        if (totalAmmo >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            totalAmmo -= ammoNeeded;
        }
        else
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }

        isReloading = false;
        UpdateAmmoUI();
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
            return;

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

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        UpdateAmmoUI();
    }

    public void AddAmmo(int ammoAmount)
    {
        totalAmmo += ammoAmount;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoDisplay.text = currentAmmo + " / " + totalAmmo;
    }

    public void SetAmmoDisplayVisibility(bool isVisible)
    {
        ammoDisplay.gameObject.SetActive(isVisible);
    }
}
