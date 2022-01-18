using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    private EnemyAnimator animatorEffects;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorEffects = GetComponent<EnemyAnimator>();

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

        Destroy(gameObject, 20f);
    }

    public void IsRunning(bool state)
    {
        animator.SetTrigger("IsRunning");
    }

    public void IsHurt()
    {
        animatorEffects.PlayHit();
        animator.SetTrigger("IsHurt");
    }

    public void IsWalking(bool state)
    {
        animator.SetTrigger("IsWalking");
    }

    public void IsAttacking(bool state)
    {
        animator.SetTrigger("IsAttacking");
    }

    public void SetMoveSpeed(Vector3 speed)
    {
        // Make this use magnitude instead to determine zombie speed
        animator.SetTrigger("moveX");
        animator.SetTrigger("moveZ");
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
                rigidbody.AddExplosionForce(force, transform.position, forceRadius);
            }
        } 
    }
}
