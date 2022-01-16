using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAnimator : MonoBehaviour
{
    [SerializeField]
    private float doorOpenSpeed = 1f;

    [SerializeField]
    private float doorCloseLimit;

    public int doorID;


    private void OnEnable()
    {
        EventManager.Instance.OnDoorOpenTriggerEnter += OpenDoor;
        EventManager.Instance.OnDoorOpenTriggerExit += CloseDoor;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDoorOpenTriggerEnter -= OpenDoor;
        EventManager.Instance.OnDoorOpenTriggerExit -= CloseDoor;
    }

    private void OpenDoor(int id)
    {
        if (id == this.doorID)
        {
            LeanTween.moveLocalY(gameObject, doorCloseLimit, doorOpenSpeed);
        }
    }

    private void CloseDoor(int id)
    {
        if (id == this.doorID)
        {
            LeanTween.moveLocalY(gameObject, 0, doorOpenSpeed);
        }
    }
};
