using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventManager : MonoBehaviour
{
    private static AnimatorEventManager _instance;
    public static AnimatorEventManager Instance { get { return _instance; } }

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


    // Player
    public event Action OnPlayerDeath;
    public event Action<bool> OnPlayerRun;
    public event Action<bool> OnPlayerWalk;
    public event Action<bool> OnPlayerShoot;
    public event Action<bool> OnPlayerJump;
    public event Action<bool> OnPlayerAim;
    public event Action<Vector2> OnPlayerSpeedChange;

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public void PlayerWalk(bool state)
    {
        OnPlayerWalk?.Invoke(state);
    }

    public void PlayerRun(bool state)
    {
        OnPlayerRun?.Invoke(state);
    }

    public void PlayerShoot(bool state)
    {
        OnPlayerShoot?.Invoke(state);
    }

    public void PlayerJump(bool state)
    {
        OnPlayerJump?.Invoke(state);
    }

    public void PlayerAiming(bool state)
    {
        OnPlayerAim?.Invoke(state);
    }

    public void SetMoveSpeed(Vector2 speed)
    {
        OnPlayerSpeedChange?.Invoke(speed);
    }


    // Enemies
    public event Action<Enemy> OnEnemyDeath;
    public event Action<Enemy> OnSuperEnemyDeath;
    public event Action<bool> OnEnemyRun;
    public event Action<bool> OnEnemyWalk;
    public event Action<bool> OnEnemyAttack;
    public event Action<Vector2> OnEnemySpeedChange;
    public event Action<Enemy, float> OnEnemyDamage;
    

    public void EnemyDeath(Enemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }
    public void SuperEnemyDeath(Enemy enemy)
    {
        OnSuperEnemyDeath?.Invoke(enemy);
    }
  
    public void EnemyDamage(Enemy enemy, float amount)
    {
        OnEnemyDamage?.Invoke(enemy, amount);
    }
}
