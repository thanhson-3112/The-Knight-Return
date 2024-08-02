using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatEnemy : EnemyBase
{
    [SerializeField] private float batHealth = 5f;
    [SerializeField] private int enemyDamage = 1;

    public override void Start()
    {
        enemyHealth = batHealth;
        damage = enemyDamage;

        base.Start(); 
    }
}
