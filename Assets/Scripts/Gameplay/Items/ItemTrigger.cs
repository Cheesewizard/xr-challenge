using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    private Pickup pickup;

    private void Awake()
    {
        pickup = GetComponent<Pickup>();      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // EventManager.StartListening("Get_Item", pickup.OnPickUp);

            // Subscribe to the events / Cant do them within the OnEnable as the script order causes a null event hander due to its initialisation
            pickup.OnPickUp += ScoreManager.Instance.UpdateScore;
            pickup.OnPickUp += StatsManager.Instance.UpdateItemsCollected;

            GameManager.Instance.AddItem(pickup.gameObject);
            pickup.GetPickedUp();
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        pickup.OnPickUp -= ScoreManager.Instance.UpdateScore;
        pickup.OnPickUp -= StatsManager.Instance.UpdateItemsCollected;
    }
}
