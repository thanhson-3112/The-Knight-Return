using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMLifeController : BossLifeBase
{
    [SerializeField] private float NMHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = NMHealth;
        damage = enemyDamage;

        base.Start();
    }
}
