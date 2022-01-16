using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager Instance { get { return _instance; } }

    // Singleton Pattern
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    // May work on later if I have time. I wanted a generic event manager that I could trigger from anywhere.
    private Dictionary<string, Action<dynamic>> EventDictionary;

    public static void TriggerEvent(string eventName, Action<dynamic> listener)
    {
        if (Instance.EventDictionary == null)
        {
            return;
        }

        if (Instance.EventDictionary.ContainsKey(eventName))
        {
            Instance.EventDictionary[eventName].Invoke(listener);
        }
    }

    public static void StartListening(string eventName, Action<dynamic> listener)
    {
        if (Instance.EventDictionary == null)
        {
            return;
        }

        if (Instance.EventDictionary.TryGetValue(eventName, out Action<dynamic> thisEvent))
        {
            // Reassign since setting it directly does not update the pointer reference.
            Instance.EventDictionary[eventName] += listener;
        }
        else
        {
            // Add event to the Dictionary for the first time
            Instance.EventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action<dynamic> listener)
    {
        if (Instance.EventDictionary == null)
        {
            return;
        }

        if (Instance.EventDictionary.TryGetValue(eventName, out Action<dynamic> thisEvent))
        {
            Instance.EventDictionary[eventName] -= listener;
        }
    }




    public event Action<int> OnDoorOpenTriggerEnter;
    public event Action<int> OnDoorOpenTriggerExit;

    public void DoorOpenTriggerEnter(int id)
    {
        OnDoorOpenTriggerEnter?.Invoke(id);
    }

    public void DoorOpenTriggerExit(int id)
    {
        OnDoorOpenTriggerExit?.Invoke(id);
    }
}
