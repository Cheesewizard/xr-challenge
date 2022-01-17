using System;

public class SuperZombie : Enemy
{
    public event Action OnSuperZombieDeath;

    private void OnEnable()
    {
        OnSuperZombieDeath += AchievementManager.Instance.SuperZombieAchievement;
    }

    private void OnDisable()
    {
        OnSuperZombieDeath -= AchievementManager.Instance.SuperZombieAchievement;
    }

    public override void Death()
    { 
        // Explode when dead, instead of dying normally.

        AnimatorEventManager.Instance.SuperEnemyDeath(this);
        OnSuperZombieDeath?.Invoke();
    }
}
