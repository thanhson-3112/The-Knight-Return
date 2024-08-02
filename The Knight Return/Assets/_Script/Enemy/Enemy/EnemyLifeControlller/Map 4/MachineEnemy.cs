using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineEnemy : EnemyBase
{
    [SerializeField] private float MachineHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public GameObject coreMachinePrefab;
    public ParticleSystem hitEffect;

    public override void Start()
    {
        enemyHealth = MachineHealth;
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
        enemyHealth = MachineHealth;
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        Instantiate(coreMachinePrefab, transform.position, transform.rotation);
    }
}
