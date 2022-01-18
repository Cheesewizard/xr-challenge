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


    private Stats stats;
    private void Start()
    {
        stats = new Stats();
    }

    public void UpdateItemsCollected(Pickup pickup)
    {
        stats.ItemsCollected += 1;
    }

    public void UpdateEnemiesKilled(Enemy enemy)
    {
        stats.EnemiesKilled += 1;
    }

    public void UpdateDamageGiven(Enemy enemy, float amount)
    {
        stats.TotalDamageGiven += amount;
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
