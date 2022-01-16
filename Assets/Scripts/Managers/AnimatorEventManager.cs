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


    public event Action OnPlayerDeath;
    public event Action<bool> OnPlayerRun;
    public event Action<bool> OnPlayerWalk;
    public event Action<bool> OnPlayerShoot;
    public event Action<bool> OnPlayerJump;
    public event Action<bool> OnPlayerAim;
    public event Action<Vector2> OnSpeedChange;

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
        OnSpeedChange?.Invoke(speed);
    }
}
