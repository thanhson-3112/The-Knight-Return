using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHLifeController : BossLifeBase
{
    [SerializeField] private float MHHealth = 5f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = MHHealth;
        damage = enemyDamage;

        base.Start();
    }
}
