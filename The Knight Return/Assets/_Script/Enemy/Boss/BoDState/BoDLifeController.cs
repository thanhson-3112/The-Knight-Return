using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDLifeController : BossLifeBase
{
    [SerializeField] public float MHHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = MHHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void EnemyHit(float _damageDone)
    {
        base.EnemyHit(_damageDone);
        bossHealth -= _damageDone;

        anim.SetTrigger("BoDTakeHit");
        anim.SetTrigger("BoDRun");

    }
}
