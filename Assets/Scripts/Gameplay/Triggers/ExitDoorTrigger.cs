using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorTrigger : MonoBehaviour
{
    public int id;
    private bool objectivesComplete;


    private void OnEnable()
    {
        GameManager.Instance.OnLevelComplete += OpenExitDoor;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelComplete -= OpenExitDoor;
    }

    private void OpenExitDoor()
    {
        this.objectivesComplete = true;
        EventManager.Instance.DoorOpenTriggerEnter(id);
    }

}

