using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGroup : MonoBehaviour
{
    private PickupAnimator pickupAnimator;

    private void Awake()
    {
        pickupAnimator = GetComponent<PickupAnimator>();
    }

    public void DestroyAfter(float seconds)
    {
        // Remove items and sub children from the scene
        Destroy(transform.gameObject, seconds);
    }

    private void OnEnable()
    {
        pickupAnimator.OnFinish += DestroyAfter;
    }

    private void OnDisable()
    {
        pickupAnimator.OnFinish -= DestroyAfter;
    }

}
