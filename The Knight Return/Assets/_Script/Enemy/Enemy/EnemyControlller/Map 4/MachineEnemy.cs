using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineEnemy : EnemyBase
{
    [SerializeField] private float MachineHealth = 3f;
    [SerializeField] private int enemyDamage = 1;

    public GameObject coreMachinePrefab;

    public override void Start()
    {
        enemyHealth = MachineHealth;
        damage = enemyDamage;

        base.Start();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        enemyHealth = MachineHealth;
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        Instantiate(coreMachinePrefab, transform.position, transform.rotation);
    }
}
