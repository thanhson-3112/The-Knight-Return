using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemyMove : EnemyMovementBase
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        
    }

    protected override void EnemyChasing()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            isChasing = false;
        }
        else
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}
