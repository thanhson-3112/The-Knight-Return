using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormExplosion : EnemyBase
{
    [SerializeField] private float WormExplosionHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public GameObject explosionPrefab;
    public ParticleSystem hitEffect;

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

    public override void TakePlayerDamage(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        base.TakePlayerDamage(_damageDone, _hitDirection, _hitForce);

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        hitEffect.Play();
    }
}
