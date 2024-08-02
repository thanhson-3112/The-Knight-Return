using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterButterflyController : EnemyNoGold
{
    [SerializeField] private float flyingHealth = 2f;
    [SerializeField] private int enemyDamage = 1;

    public ParticleSystem hitEffect;

    public override void Start()
    {
        enemyHealth = flyingHealth;
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
        enemyHealth = flyingHealth;
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        Destroy(gameObject);
    }
}
