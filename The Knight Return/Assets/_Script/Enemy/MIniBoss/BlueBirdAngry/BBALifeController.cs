using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBALifeController : BossLifeBase
{
    [SerializeField] private float BBAHealth = 5f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = BBAHealth;
        damage = enemyDamage;

        base.Start();
    }
}
