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
}
