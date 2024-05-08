using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    [SerializeField] private float flyingHealth = 2f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = flyingHealth;
        damage = enemyDamage;

        base.Start();
    }
}
