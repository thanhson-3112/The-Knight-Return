using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomWorm : EnemyBase
{
    [SerializeField] private float boomWormHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public GameObject explosionPrefab;
    public ParticleSystem hitEffect;

    public override void Start()
    {
        enemyHealth = boomWormHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void TakePlayerDamage(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        base.TakePlayerDamage(_damageDone, _hitDirection, _hitForce);
        hitEffect.Play();

    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        enemyHealth = boomWormHealth;
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
