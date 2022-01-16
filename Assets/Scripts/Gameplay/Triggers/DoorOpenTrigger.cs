using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider collision)
    {
        EventManager.Instance.DoorOpenTriggerEnter(id);
    }

    private void OnTriggerExit(Collider collision)
    {
        EventManager.Instance.DoorOpenTriggerExit(id);
    }
}
