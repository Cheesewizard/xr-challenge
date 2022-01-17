using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    public int damage = 10;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        FireGun();
        Reload();
        AimGun();
    }


    public void FireGun()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Shoot
            Shoot();
            DecreaseAmmo();
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
        }

        // When released set player aiming to false
        if (Input.GetButtonUp("Fire2"))
        {
            AnimatorEventManager.Instance.PlayerAiming(false);
        }


    }

    private void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            Debug.Log(hit.transform.name);
            var target = hit.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reload
        }
    }

    public void DecreaseAmmo()
    {

    }

    public void AddAmmo()
    {

    }
}
