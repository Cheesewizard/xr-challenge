using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammoAmount = 32;
    private Pickup pickup;
    public Transform weapon;
    private Ammo ammo;

    private void Awake()
    {
        pickup = GetComponent<Pickup>();
        ammo = weapon.gameObject.GetComponent<Ammo>();
    }

    /// <summary>
    /// Cannot put the events in the enable function because Init() function inside pickup.cs has not run at that point causing it to reset to null.
    /// </summary>
    private void SubscribeEvents()
    {
        // Subscribe to events
        pickup.OnPickUp += StatsManager.Instance.UpdateItemsCollected;

        ammo.onUpdateClip += AmmoManager.Instance.UpdateAmmoUi;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        pickup.OnPickUp -= StatsManager.Instance.UpdateItemsCollected;

        ammo.onUpdateClip += AmmoManager.Instance.UpdateAmmoUi;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SubscribeEvents();
            pickup.GetPickedUp();

            var ammo = weapon.GetComponent<Ammo>();
            ammo.AddAmmo(ammoAmount);
        }
    }
}
