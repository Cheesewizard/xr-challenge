using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private static StatsManager _instance;
    public static StatsManager Instance { get { return _instance; } }

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


   // public event Action 


    private Stats stats;
    private void Start()
    {
        stats = new Stats();
    }

    public void UpdateStats(Pickup pickup)
    {
        stats.ItemsCollected += 1;
        stats.ItemsCollected += 1;
    }
}
