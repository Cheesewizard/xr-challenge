using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IKillable
{
    public float Health;
    public float Power;
    public float Speed;
    public bool IsDead;

    public virtual void TakeDamage(float amount, float gunForce, float forceRadius)
    {
        if (!IsDead)
        {
            Health -= amount;
            AnimatorEventManager.Instance.EnemyDamage(this, amount);

            if (Health <= 0)
            {
                Death(gunForce, forceRadius);
            }
        }
    }

    public virtual void Death(float gunForce, float forceRadius)
    {
        IsDead = true;
        AnimatorEventManager.Instance.EnemyDeath(this, gunForce, forceRadius);
    }

}
