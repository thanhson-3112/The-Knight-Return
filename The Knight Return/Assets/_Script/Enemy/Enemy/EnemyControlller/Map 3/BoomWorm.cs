using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomWorm : EnemyBase
{
    [SerializeField] private float boomWormHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public GameObject explosionPrefab;

    public override void Start()
    {
        enemyHealth = boomWormHealth;
        damage = enemyDamage;

        base.Start();
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
