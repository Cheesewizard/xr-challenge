using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField]

    private ParticleSystem deathEffect;
    [SerializeField]
    private ParticleSystem[] damageEffect;

    [SerializeField]
    private ParticleSystem attackEffect;
    [SerializeField]
    private Transform attackOrigin;

    [SerializeField]
    private float particleHeightOffset;
    private bool isDead;

    public void PlayDeath()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        Instantiate(deathEffect, GetheightOffset(gameObject), deathEffect.gameObject.transform.rotation);
        deathEffect.Play();
    }

    public void PlayHit()
    {
        if (isDead)
        {
            return;
        }

        // Spawn a random hit effect from the particle array
        Instantiate(damageEffect[UnityEngine.Random.Range(0, damageEffect.Length)], GetheightOffset(gameObject), gameObject.transform.rotation);
    }

    public void PlayAttack()
    {
        Instantiate(attackEffect, attackOrigin.position, attackEffect.gameObject.transform.rotation);
        attackEffect.Play();
    }

    /// <summary>
    /// Allows the ability to customize where particle spawn since some assets may have the pivot set at the feet.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    private Vector3 GetheightOffset(GameObject gameObject)
    {
        return gameObject.transform.position + new Vector3(0, particleHeightOffset, 0);
    }
}
