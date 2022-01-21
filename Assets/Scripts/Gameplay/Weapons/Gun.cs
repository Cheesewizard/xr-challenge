using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : Ammo
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
    public Transform bulletsSpawn;

    [Header("Variables")]
    public float fireRate = 0.5f;
    public int damage = 10;
    public float gunForce = 100f;
    public float forceRadius = 50f;

   
    public int costPerBullet = 1;

    public int clipSize = 32;

    public float reloadTime = 1.5f;

    // Used to cause bullet delays before firing again
    private float nextTimeToFire;
    public bool isReloading;

    private void Start()
    {     
        // Start with full ammo
        currentAmmoClip = clipSize;

        // Update the UI for whatever the current value is at start
        onUpdateClip?.Invoke(currentAmmoClip, totalAmmunition);
    }


    /// <summary>
    /// When fire1 is pressed the gun will fire. 
    /// The gun will only fire when the user is also aiming
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// When fire2 is pressed the characterm aims down the gun.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Prcesses the bullet physics using a raycast and processes the bullet effects
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="bulletsSpawn"></param>
    public void Shoot(Vector3 direction, Transform bulletsSpawn)
    {
        if (direction == Vector3.zero)
        {
            // Get the direction facing from the gun barrel, not been adjusted
            direction = bulletsSpawn.transform.up;
        }

        // Not clean code
        muzleFlashParticleEffect.Play();

        if (Physics.Raycast(bulletsSpawn.transform.position, direction, out RaycastHit hit, float.MaxValue, layerMask))
        {
            var trail = Instantiate(bulletEffect, bulletsSpawn.transform.position, Quaternion.identity);

            // Spawn a hit effect at the point the trail hits the raycast point
            StartCoroutine(SpawnBulletEffect(trail, hit, impactParticleEffect));

            var target = hit.transform.GetComponent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage, gunForce, forceRadius);
            }
        }
    }

    /// <summary>
    /// Instantiates the bullet trail effect
    /// </summary>
    /// <param name="trail"></param>
    /// <param name="hit"></param>
    /// <param name="impactParticleEffect"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Checks to see if the user is currently pressing the reload button
    /// </summary>
    public void CheckForReload()
    {
        // This logic does not take into account at what point in the animation do we give the player their ammo back
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(DoReload());
        }
    }

    /// <summary>
    /// This functions starts the reload animation
    /// </summary>
    /// <returns></returns>
    public IEnumerator DoReload()
    {
        // This flow should be improved

        if (CheckIfCanReload(clipSize))
        {
            isReloading = true;
            AnimatorEventManager.Instance.PlayerReload();
            AnimatorEventManager.Instance.PlayerHasReloaded(false);

            // Wait for x seconds
            yield return new WaitForSeconds(reloadTime);
            ReloadFromAmmo(clipSize);
            isReloading = false;
        }

        yield return new WaitForSeconds(0);

    }
}
