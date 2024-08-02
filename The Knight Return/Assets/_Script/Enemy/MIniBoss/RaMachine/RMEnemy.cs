using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMEnemy : BossLifeBase
{
    [SerializeField] public float RAHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = RAHealth;
        damage = enemyDamage;

        base.Start();
    }
}

