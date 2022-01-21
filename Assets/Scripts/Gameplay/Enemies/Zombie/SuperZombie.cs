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

    public override void TakeDamage(float amount, float gunForce, float forceRadius)
    {
        if (!IsDead)
        {
            Health -= amount;
            AnimatorEventManager.Instance.EnemyDamage(this, amount);

            if (Health <= 0)
            {
                Death(gunForce, forceRadius);
                OnSuperZombieDeath?.Invoke();
            }
        }
    }
}
