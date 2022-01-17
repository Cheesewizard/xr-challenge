using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IKillable
{
    public float Health;
    public float Power;

    public virtual void TakeDamage(float amount)
    {
        Health -= amount;
        AnimatorEventManager.Instance.EnemyDamage(this, amount);

        if (Health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        AnimatorEventManager.Instance.EnemyDeath(this);
    }

}
