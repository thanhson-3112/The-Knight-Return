using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWorm : EnemyBase
{
    [SerializeField] private float platformWorm = 1000f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = platformWorm;
        damage = enemyDamage;

        base.Start();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        enemyHealth = platformWorm;
    }
}
