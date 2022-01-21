using System;
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


    private void OnEnable()
    {
        AnimatorEventManager.Instance.OnEnemyDeath += Instance.UpdateEnemiesKilled;
        AnimatorEventManager.Instance.OnEnemyDamage += Instance.UpdateDamageGiven;
    }


    private void OnDisable()
    {
        AnimatorEventManager.Instance.OnEnemyDeath -= Instance.UpdateEnemiesKilled;
        AnimatorEventManager.Instance.OnEnemyDamage -= Instance.UpdateDamageGiven;
    }

    // Events
    public Action<int> onItemsCollected;
    public Action<int> onEnemysKilled;
    public Action<int> onDamageGiven;


    private Stats stats;
    private void Start()
    {
        stats = new Stats();
    }

    public void UpdateItemsCollected(Pickup pickup)
    {
        stats.ItemsCollected += 1;
        onItemsCollected?.Invoke(stats.ItemsCollected);
    }

    public void UpdateEnemiesKilled(Enemy enemy)
    {
        stats.EnemiesKilled += 1;
        onEnemysKilled?.Invoke(stats.EnemiesKilled);
    }

    public void UpdateDamageGiven(Enemy enemy, float amount)
    {
        stats.TotalDamageGiven += amount;
        onDamageGiven?.Invoke((int)stats.TotalDamageGiven);
    }

    public void UpdateDamageTaken(float amount)
    {
        stats.TotalDamageTaken += amount;
    }



    //public void UpdateWeaponStats(IWeapon gun)
    //{
    //    stats.TotalBulletsFiredCount
    //}
}
