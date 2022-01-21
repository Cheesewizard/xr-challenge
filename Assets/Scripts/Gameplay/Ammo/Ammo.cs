using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    [Header("Ammunition")]
    public int currentAmmoClip;
    public int totalAmmunition = 15000;

    // Events
    public Action<int, int> onUpdateClip;
    public Action<int> onUpdateTotalAmmo;


    private void OnEnable()
    {
        onUpdateClip += AmmoManager.Instance.UpdateAmmoUi;
    }

    private void OnDisable()
    {
        onUpdateClip -= AmmoManager.Instance.UpdateAmmoUi;
    }



    /// <summary>
    /// This checks to see if the user is able to reload and returns a true or false.
    /// </summary>
    /// <returns></returns>
    public bool CheckIfCanReload(int clipSize)
    {
        if (currentAmmoClip < clipSize && totalAmmunition > 1)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This processes the logic for moving ammo from an ammo pile when reloading
    /// </summary>
    public void ReloadFromAmmo(int clipSize)
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

        onUpdateClip?.Invoke(currentAmmoClip, totalAmmunition);
    }

    /// <summary>
    /// Decreases the total ammo count
    /// </summary>
    public virtual void DecreaseAmmo()
    {
        currentAmmoClip -= 1;
        onUpdateClip?.Invoke(currentAmmoClip, totalAmmunition);
    }

    /// <summary>
    /// Increases the total ammo count
    /// </summary>a
    /// <param name="ammoAmount"></param>
    public void AddAmmo(int ammoAmount)
    {
        totalAmmunition += ammoAmount;
        onUpdateClip?.Invoke(currentAmmoClip, totalAmmunition);
    }
}
