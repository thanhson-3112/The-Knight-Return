using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskLifeController : BossLifeBase
{
    [SerializeField] private float TuskHealth = 5f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = TuskHealth;
        damage = enemyDamage;

        base.Start();
    }
}
