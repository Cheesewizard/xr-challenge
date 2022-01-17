using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem deathEffect;
    [SerializeField]
    private ParticleSystem superDeathEffect;
    [SerializeField]
    private ParticleSystem damageEffect;

    public float particleHeightOffset;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Register events
        AnimatorEventManager.Instance.OnEnemyDeath += Kill;
        AnimatorEventManager.Instance.OnSuperEnemyDeath += KillSuper;
        AnimatorEventManager.Instance.OnEnemyRun += IsRunning;
        AnimatorEventManager.Instance.OnEnemyWalk += IsWalking;
        AnimatorEventManager.Instance.OnEnemySpeedChange += SetMoveSpeed;
        AnimatorEventManager.Instance.OnEnemyAttack += IsAttacking;
        AnimatorEventManager.Instance.OnEnemyDamage += IsHurt;
    }

    private void OnDisable()
    {
        AnimatorEventManager.Instance.OnEnemyDeath -= Kill;
        AnimatorEventManager.Instance.OnSuperEnemyDeath -= KillSuper;
        AnimatorEventManager.Instance.OnEnemyRun -= IsRunning;
        AnimatorEventManager.Instance.OnEnemyWalk -= IsWalking;
        AnimatorEventManager.Instance.OnEnemySpeedChange -= SetMoveSpeed;
        AnimatorEventManager.Instance.OnEnemyAttack -= IsAttacking;
        AnimatorEventManager.Instance.OnEnemyDamage -= IsHurt;
    }


    private void SpawnParticleEffectAt(ParticleSystem particle, Transform target, float height)
    {
        // Some enemies may have pivot points at the feet rather than the center, so one can adjust it now
        var position = new Vector3(target.position.x, transform.position.y * height, transform.position.z);

        // Create particle effect at the enemy position
        Instantiate(particle, position, Quaternion.identity).Play();
    }

    public void Kill(Enemy enemy)
    {
        SpawnParticleEffectAt(deathEffect, enemy.transform, particleHeightOffset);
        animator.SetTrigger("IsDead");
    }

    public void KillSuper(Enemy enemy)
    {
        SpawnParticleEffectAt(superDeathEffect, enemy.transform, particleHeightOffset);
        animator.SetTrigger("IsDead");
    }

    public void IsRunning(bool state)
    {
        animator.SetBool("IsRunning", state);
    }

    public void IsHurt(Enemy enemy, float amount)
    {
        SpawnParticleEffectAt(damageEffect, enemy.transform, particleHeightOffset);
        animator.SetTrigger("IsHurt");
    }

    public void IsWalking(bool state)
    {
        animator.SetBool("IsWalking", state);
    }

    public void IsAttacking(bool state)
    {
        animator.SetBool("IsAttacking", state);
    }


    public void SetMoveSpeed(Vector2 speed)
    {
        // Make this use magnitude instead to determine zombie speed
        animator.SetFloat("moveX", speed.x, 0.1f, Time.deltaTime);
        animator.SetFloat("moveY", speed.y, 0.1f, Time.deltaTime);
    }
}
