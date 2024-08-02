using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterButterflyController : EnemyBase
{
    [SerializeField] private float flyingHealth = 2f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = flyingHealth;
        damage = enemyDamage;

        base.Start();
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
