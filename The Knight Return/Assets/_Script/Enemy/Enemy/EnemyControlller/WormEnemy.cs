using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : EnemyBase
{
    [SerializeField] private float wormHealth = 2f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = wormHealth;
        damage = enemyDamage;

        base.Start();
    }
}
