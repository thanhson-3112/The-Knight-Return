using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasrEnemyMove : EnemyMovementBase
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
                transform.position += Vector3.left * moveSpeed * 2f * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.right * moveSpeed * 2f * Time.deltaTime;
            }
        }
    }
}
