using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : EnemyBase
{
    [SerializeField] public float wormHealth = 2f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = wormHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        enemyHealth = wormHealth;

    }
}
