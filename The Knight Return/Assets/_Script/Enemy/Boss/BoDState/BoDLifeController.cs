using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDLifeController : BossLifeBase
{
    [SerializeField] public float BoDHealth = 20f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        bossHealth = BoDHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void TakePlayerDamage(float _damageDone)
    {
        base.TakePlayerDamage(_damageDone);
        bossHealth -= _damageDone;

        anim.SetTrigger("BoDTakeHit");
        anim.SetTrigger("BoDRun");

    }
}
