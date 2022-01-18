using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [Header("RayCast")]
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask layerMask;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem impactParticleEffect;

    [SerializeField]
    private ParticleSystem muzleFlashParticleEffect;

    [SerializeField]
    private TrailRenderer bulletEffect;

    [Header("Gun Position")]
    public Transform gunPosition;

    [Header("Gun Variables")]
    public float fireRate = 0.5f;
    public int damage = 10;
    public float gunForce = 100f;
    public float forceRadius = 50f;

    public int totalAmmunition;
    public int costPerBullet = 1;

    public int clipSize = 32;
    public int currentAmmoClip;
    public float reloadTime = 1.5f;

    // Used to cause bullet delays before firing again
    private float nextTimeToFire;
    public bool isReloading;

    private void Start()
    {
        // Start with full ammo
        currentAmmoClip = clipSize;
    }


    public bool FireGun()
    {
        if (Time.time >= nextTimeToFire)
        {
            if (Input.GetButton("Fire1") && currentAmmoClip > 0)
            {
                // Sets a delay on how much you can fire the gun
                nextTimeToFire = Time.time + fireRate * 0.5f;

                // Set Player shooting animation
                AnimatorEventManager.Instance.PlayerShoot(true);
                return true;
            }
        }
        // When released set player shooting to false
        AnimatorEventManager.Instance.PlayerShoot(false);
        return false;
    }
    public bool AimGun()
    {
        var aimPressed = Input.GetButton("Fire2");
        if (aimPressed)
        {
            // Aim animation
            AnimatorEventManager.Instance.PlayerAiming(true);
            return true;
        }

        // When released set player aiming to false
        AnimatorEventManager.Instance.PlayerAiming(false);
        AnimatorEventManager.Instance.PlayerShoot(false);

        return false;
    }
    public void Shoot(Vector3 direction, Transform gunPosition)
    {
        if (direction == Vector3.zero)
        {
            // Get the direction facing from the gun barrel, not been adjusted
            direction = gunPosition.transform.up;
        }

        // Not clean code
        muzleFlashParticleEffect.Play();

        if (Physics.Raycast(gunPosition.transform.position, direction, out RaycastHit hit, float.MaxValue, layerMask))
        {
            var trail = Instantiate(bulletEffect, gunPosition.transform.position, Quaternion.identity);

            // Spawn a hit effect at the point the trail hits the raycast point
            StartCoroutine(SpawnBulletEffect(trail, hit, impactParticleEffect));

            var target = hit.transform.GetComponent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage, gunForce, forceRadius);
            }
        }
    }

    public IEnumerator SpawnBulletEffect(TrailRenderer trail, RaycastHit hit, ParticleSystem impactParticleEffect)
    {
        var time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            // Animate the trail between two points
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;

        // Spawn hit effect
        Instantiate(impactParticleEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(trail.gameObject, trail.time);
    }

    public void CheckForReload()
    {
        // This logic does not take into account at what point in the animation do we give the player their ammo back
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(DoReload());
        }
    }

    public IEnumerator DoReload()
    {
        // This flow should be improved

        if (CheckIfCanReload())
        {
            isReloading = true;
            AnimatorEventManager.Instance.PlayerReload(true);
            AnimatorEventManager.Instance.PlayerHasReloaded(false);
                  
            // Wait for x seconds
            yield return new WaitForSeconds(reloadTime);
            ReloadFromAmmo();
            AnimatorEventManager.Instance.PlayerReload(false);
            isReloading = false;
        }

        yield return new WaitForSeconds(0);

    }
    private bool CheckIfCanReload()
    {
        if (currentAmmoClip < clipSize && totalAmmunition > 1)
        {
            return true;
        }
        return false;
    }

    private void ReloadFromAmmo()
    {
        if (currentAmmoClip < 0)
        {
            // This would indicate an empty clip
            currentAmmoClip = 0;
        }

        var emptyBulletSlots = clipSize - currentAmmoClip;

        // There exists enough bullets in our ammo pile
        if (totalAmmunition / emptyBulletSlots >= 1)
        {
            // Add the remaining bullets, to fill up the clip to full
            currentAmmoClip = clipSize;
            totalAmmunition -= emptyBulletSlots;
        }
        else
        {
            // Add the remaining amound of our ammo pile to our current ammo clip
            currentAmmoClip += totalAmmunition;
            totalAmmunition -= totalAmmunition;
        }
    }

    public virtual void DecreaseAmmo()
    {
        if (isReloading)
        {
            return;
        }

        currentAmmoClip -= 1;
        if (currentAmmoClip <= 0)
        {
            StartCoroutine(DoReload());
        }
    }


    public void AddAmmo(int ammoAmount)
    {
        totalAmmunition += ammoAmount;
    }
}
