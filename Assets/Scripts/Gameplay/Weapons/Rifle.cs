using UnityEngine;

public class Rifle : Gun
{
    [Header("Bullet Spread")]
    [SerializeField]
    private bool addBulletSpread = true;

    [SerializeField]
    private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        CheckForReload();
        if (AimGun())
        {
            if (FireGun())
            {
                Shoot(GetDirection(), gunPosition);
                DecreaseAmmo();
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = gunPosition.transform.up;

        if (addBulletSpread)
        {
            direction += new Vector3(

                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                 Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                  Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
                  );

            direction.Normalize();
        }

        return direction;
    }

    private void LazerSight()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Could add a change here to have different ammo types instead of just one.
        if (collision.gameObject.CompareTag("Ammo"))
        {
            // Debugging only, this should pull the ammo type and then use that value. May not have time to implement
            AddAmmo(20);
        }
    }

}
