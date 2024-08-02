using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHLifeController : BossLifeBase
{
    [SerializeField] public float MHHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = MHHealth;
        damage = enemyDamage;

        base.Start();
    }
}
