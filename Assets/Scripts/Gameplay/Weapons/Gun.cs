using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask layerMask;

    public int damage = 10;
    public float gunForce = 100f;
    public float forceRadius = 50f;

    [SerializeField]
    private GameObject gunPosition;

    [SerializeField]
    private bool AddBulletSpread = true;

    [SerializeField]
    private Vector3 BulletSpreadVarience = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField]
    private bool ShootingSystem = true;

    [SerializeField]
    private TrailRenderer BulletTrail;

    [SerializeField]
    private float ShootDelay = 0.5f;

    [SerializeField]
    private ParticleSystem ImpactParticleSystem;

    [SerializeField]
    private ParticleSystem muzleFlashParticleEffect;

    [SerializeField]
    private float LastShootTime;



    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        Reload();
        AimGun();
    }


    public void FireGun()
    {
        if (Input.GetButton("Fire1"))
        {
            // Shoot
            Shoot();
            DecreaseAmmo();
            AnimatorEventManager.Instance.PlayerShoot(true);
            return;
        }

        // When released set player shooting to false
        if (Input.GetButtonUp("Fire1"))
        {
            AnimatorEventManager.Instance.PlayerShoot(false);
        }
    }

    public void AimGun()
    {
        var aimPressed = Input.GetButton("Fire2");
        if (aimPressed)
        {
            // Lazer sight
            // Aim animation
            AnimatorEventManager.Instance.PlayerAiming(true);
            FireGun();
        }

        // When released set player aiming to false
        if (Input.GetButtonUp("Fire2"))
        {
            AnimatorEventManager.Instance.PlayerAiming(false);
        }
    }

    private void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            Debug.DrawRay(gunPosition.transform.position, gunPosition.transform.up * 200f, Color.green);

            // Not clean code
            muzleFlashParticleEffect.Play();

            var direction = GetDirection(); //gunPosition.transform.up;
            if (Physics.Raycast(gunPosition.transform.position, direction, out RaycastHit hit, float.MaxValue, layerMask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, gunPosition.transform.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));
                LastShootTime = Time.time;


                Debug.Log(hit.transform.name);
                var target = hit.transform.GetComponent<Enemy>();

                if (target != null)
                {
                    target.TakeDamage(damage, gunForce, forceRadius);
                }
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = gunPosition.transform.up;

        if (AddBulletSpread)
        {
            direction += new Vector3(

                Random.Range(-BulletSpreadVarience.x, BulletSpreadVarience.x),
                 Random.Range(-BulletSpreadVarience.y, BulletSpreadVarience.y),
                  Random.Range(-BulletSpreadVarience.z, BulletSpreadVarience.z)
                  );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;
        Instantiate(ImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);

    }

    private void LazerSight()
    {

    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnimatorEventManager.Instance.PlayerReload(true);
            AnimatorEventManager.Instance.PlayerHasReloaded(false);
            return;
        }

        AnimatorEventManager.Instance.PlayerReload(false);
    }

    public void DecreaseAmmo()
    {

    }

    public void AddAmmo()
    {

    }
}
