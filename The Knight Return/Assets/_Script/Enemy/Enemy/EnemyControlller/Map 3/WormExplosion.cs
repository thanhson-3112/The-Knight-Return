using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormExplosion : EnemyBase
{
    [SerializeField] private float WormExplosionHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public GameObject explosionPrefab;

    public override void Start()
    {
        enemyHealth = WormExplosionHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        enemyHealth = WormExplosionHealth;
    }

    public override void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        base.EnemyHit(_damageDone, _hitDirection, _hitForce);

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        enemyHealth -= _damageDone;

        if (!isRecolling)
        {
            rb.AddForce(-_hitForce * recollFactor * _hitDirection);
            isRecolling = true;
        }

        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }
}
