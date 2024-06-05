using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMLifeController : BossLifeBase
{
    [SerializeField] private float FPHealth = 5f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = FPHealth;
        damage = enemyDamage;

        base.Start();
    }
}
