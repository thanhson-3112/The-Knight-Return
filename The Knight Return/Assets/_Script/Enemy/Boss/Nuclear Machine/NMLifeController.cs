using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMLifeController : BossLifeBase
{
    [SerializeField] private float NMHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public ParticleSystem hitEffect;
    public ParticleSystem hitEffect1;

    public override void Start()
    {
        bossHealth = NMHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void TakePlayerDamage(float _damageDone)
    {
        base.TakePlayerDamage(_damageDone);
        hitEffect.Play();

    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        hitEffect1.Play();
    }
}
