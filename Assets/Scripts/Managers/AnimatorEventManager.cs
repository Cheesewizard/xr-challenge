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
  
    // Movement
    public event Action<bool> OnPlayerRun;
    public event Action<bool> OnPlayerWalk;
    public event Action<bool> OnPlayerJump;
    public event Action<bool> OnPlayerTouchGround;
    public event Action<Vector3> OnPlayerSpeedChange;

    // Health
    public event Action<bool> OnPlayerHurt;
    public event Action OnPlayerDeath;

    // Weapon
    public event Action<bool> OnPlayerReload;
    public event Action<bool> OnPlayerHasReloaded;
    public event Action<bool> OnPlayerAim;
    public event Action<bool> OnPlayerShoot;



    public void PlayerWalk(bool state)
    {
        OnPlayerWalk?.Invoke(state);
    }

    public void PlayerRun(bool state)
    {
        OnPlayerRun?.Invoke(state);
    }

    public void PlayerJump(bool state)
    {
        OnPlayerJump?.Invoke(state);
    }

    public void PlayerTouchGround(bool state)
    {
        OnPlayerTouchGround?.Invoke(state);
    }

    public void SetMoveSpeed(Vector3 speed)
    {
        OnPlayerSpeedChange?.Invoke(speed);
    }

    public void PlayerHurt(bool state)
    {
        OnPlayerHurt?.Invoke(state);
    }

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }


    public void PlayerShoot(bool state)
    {
        OnPlayerShoot?.Invoke(state);
    }

    public void PlayerAiming(bool state)
    {
        OnPlayerAim?.Invoke(state);
    }

    public void PlayerReload(bool state)
    {
        OnPlayerReload?.Invoke(state);
    }

    public void PlayerHasReloaded(bool state)
    {
        OnPlayerHasReloaded?.Invoke(state);
    }




    // Enemies
    public event Action<Enemy> OnEnemyDeath;
    public event Action<bool> OnEnemyRun;
    public event Action<bool> OnEnemyWalk;
    public event Action<bool> OnEnemyAttack;
    public event Action<Vector3> OnEnemySpeedChange;
    public event Action<Enemy, float> OnEnemyDamage;


    public void EnemyDeath(Enemy enemy, float gunForce, float forceRadius)
    {
        var enemyAnimatorController = enemy.GetComponent<EnemyAnimatorController>();
        enemyAnimatorController.Kill(gunForce, forceRadius);
        OnEnemyDeath?.Invoke(enemy);
    }

    public void EnemyDamage(Enemy enemy, float amount)
    {
        var enemyAnimatorController = enemy.GetComponent<EnemyAnimatorController>();
        enemyAnimatorController.IsHurt();
        OnEnemyDamage?.Invoke(enemy, amount);
    }

    //// Register events
    //AnimatorEventManager.Instance.OnEnemyDeath += Kill;
    //    AnimatorEventManager.Instance.OnEnemyRun += IsRunning;
    //    AnimatorEventManager.Instance.OnEnemyWalk += IsWalking;
    //    AnimatorEventManager.Instance.OnEnemySpeedChange += SetMoveSpeed;
    //    AnimatorEventManager.Instance.OnEnemyAttack += IsAttacking;
    //    AnimatorEventManager.Instance.OnEnemyDamage += IsHurt
}
