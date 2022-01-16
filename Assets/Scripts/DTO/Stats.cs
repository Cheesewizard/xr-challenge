using System;
using System.Diagnostics;
using UnityEngine;

public class Stats
{
    private Stopwatch stopwatch;
    public Stats()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    public int ItemsCollected { get; set; } = 0;
    public string TimePlayed
    {
        get
        {
            return $"{stopwatch.Elapsed.TotalHours}:{stopwatch.Elapsed.TotalMinutes}:{stopwatch.Elapsed.TotalSeconds}";
        }
    }

    public int EnemiesKilled { get; set; } = 0;
    public int JumpCount { get; set; } = 0;
    public GameObject[] ItemsRemaining { get; set; }
}
