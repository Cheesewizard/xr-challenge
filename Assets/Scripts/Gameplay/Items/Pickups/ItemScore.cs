using UnityEngine;

public class ItemScore : Pickup
{
    private Pickup pickup;

    private void Awake()
    {
        pickup = GetComponent<Pickup>();
    }

    /// <summary>
    /// Cannot put the events in the enable function because Init() function inside pickup.cs has not run at that point causing it to reset to null.
    /// </summary>
    private void SubscribeEvents()
    {
        // Subscribe to events
        pickup.OnPickUp += ScoreManager.Instance.UpdateScore;
        pickup.OnPickUp += StatsManager.Instance.UpdateItemsCollected;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        pickup.OnPickUp -= ScoreManager.Instance.UpdateScore;
        pickup.OnPickUp -= StatsManager.Instance.UpdateItemsCollected;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SubscribeEvents();
            pickup.GetPickedUp();
        }
    }
}
