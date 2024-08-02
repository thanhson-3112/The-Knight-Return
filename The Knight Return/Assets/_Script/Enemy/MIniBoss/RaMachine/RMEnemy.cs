using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMEnemy : BossLifeBase
{
    [SerializeField] public float RAHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public ParticleSystem hitEffect;
    public ParticleSystem hitEffect1;

    [Header("Sound")]
    public AudioClip deathSound;

    public override void Start()
    {
        bossHealth = RAHealth;
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
        SoundFxManager.instance.PlaySoundFXClip(deathSound, transform, 1f);

    }
}

