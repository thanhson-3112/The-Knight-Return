using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastWormEnemy : EnemyBase
{
    [SerializeField] private float FastWormHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = FastWormHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        enemyHealth = FastWormHealth;
    }
}
