using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMLifeController : BossLifeBase
{
    [SerializeField] private float PMHealth = 25f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = PMHealth;
        damage = enemyDamage;

        base.Start();
    }
}
