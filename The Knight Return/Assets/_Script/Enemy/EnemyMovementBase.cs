using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBase : MonoBehaviour
{
    public Transform[] movePoint;
    public float moveSpeed = 2;
    public int moveDestination;

    // Follow player
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance = 3;

    // kich thuoc quai
    [Header("EnemyScale")]
    public float enemyScaleX = 9;
    public float enemyScaleY = 9;
    public float enemyScaleZ = 9;

    protected virtual void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        EnemyMove();
    }

    protected virtual void EnemyMove()
    {
        if (isChasing)
        {
            EnemyChasing();
        }
        else
        {
            EnemyNotChasing();
        }
    }

    protected virtual void EnemyChasing()
    {
        
    }

    protected virtual void EnemyNotChasing()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            isChasing = true;
        }
        if (moveDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoint[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, movePoint[0].position) < .2f)
            {
                transform.localScale = new Vector3(enemyScaleX, enemyScaleY, enemyScaleZ);
                moveDestination = 1;
            }
        }
        if (moveDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoint[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, movePoint[1].position) < .2f)
            {
                transform.localScale = new Vector3(-enemyScaleX, enemyScaleY, enemyScaleZ);
                moveDestination = 0;
            }
        }
    }
}
