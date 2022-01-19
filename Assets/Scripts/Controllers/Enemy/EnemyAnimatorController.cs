using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    private EnemyAnimator animatorEffects;
    private ZombieAIController aiController;

    // Start is called before the first frame update
    void Start()
    {
        // Sound effects here
        // audioController = 
        animator = GetComponent<Animator>();
        animatorEffects = GetComponent<EnemyAnimator>();
        aiController = GetComponent<ZombieAIController>();

        // Setup ragdoll physics on enemy
        SetRigidBodyState(true);
        SetColliderState(false);
    }

    public void Kill(float gunForce, float forceRadius)
    {
        animatorEffects.PlayDeath();
        animator.SetTrigger("IsDead");

        // Enable ragdoll physics
        animator.enabled = false;
        SetRigidBodyState(false);
        SetColliderState(true);
        AppplyForce(gunForce, forceRadius);

        Destroy(aiController);
        Destroy(gameObject, 30f);
    }

    public void IsHurt()
    {
        animatorEffects.PlayHit();
        animator.SetTrigger("IsHurt");
    }

    public void IsWalking(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    public void IsAttacking()
    {
        animator.SetTrigger("IsAttacking");
    }

    private void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }


    private void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

    private void AppplyForce(float force, float forceRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, forceRadius);
        foreach (var collider in colliders)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if(rigidbody != null)
            {
                // rigidbody.AddExplosionForce(force, transform.position, forceRadius);
                rigidbody.AddExplosionForce(force, transform.position, forceRadius, force * 0.2f, ForceMode.Impulse);
            }
        } 
    }
}
